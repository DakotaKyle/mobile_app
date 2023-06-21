using mobile_app.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace mobile_app.Services
{
    public static class DatabaseService
    {
        private static SQLiteAsyncConnection _db;

        static async Task Init()
        {
            if(_db != null) // Don't create the database if it already exists.
            {
                return;
            }

            //get an absolute path to the database file.
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Gadgets.db");

            _db = new SQLiteAsyncConnection(databasePath);

            await _db.CreateTableAsync<Gadget>();
            await _db.CreateTableAsync<Widget>();
        }

        #region Gadget methods

        public static async Task AddGadget(string name, string color)
        {
            await Init();

            var gadget = new Gadget()
            {
                Name = name,
                Color = color
            };

            await _db.InsertAsync(gadget);
            var id = gadget.Id; //returns gadget ID.
        }

        public static async Task RemoveGadget(int id)
        {
            await Init();
            await _db.DeleteAsync(id);
        }

        public static async Task<IEnumerable<Gadget>> GetGadgets()
        {
            await Init();

            var gadgets = await _db.Table<Gadget>().ToListAsync();
            return gadgets;
        }

        public static async Task UpdateGadget(int id, string name, string color)
        {
            await Init();

            var gadgetQuery = await _db.Table<Gadget>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (gadgetQuery != null)
            {
                gadgetQuery.Name = name;
                gadgetQuery.Color = color;

                await _db.UpdateAsync(gadgetQuery);
            }
        }

        #endregion

        #region Widget methods

        public static async Task AddWidget(int gadgetId, string name, string color, bool notificationStart, string notes)
        {
            await Init();
            var widget = new Widget()
            {
                GadgetId = gadgetId,
                Name = name,
                Color = color,
                StartNotification = notificationStart,
                Notes = notes
            };
            await _db.InsertAsync(widget);
            var id = widget.Id; // Returns widget ID.
        }

        public static async Task RemoveWidget(int id)
        {
            await Init();
            await _db.DeleteAsync<Widget>(id);
        }

        public static async Task<IEnumerable<Widget>> GetWidgets(int gadgetId)
        {
            await Init();

            var widget = await _db.Table<Widget>().Where(i => i.GadgetId == gadgetId).ToListAsync();

            return widget;
        }

        public static async Task<IEnumerable<Widget>> GetWidgets()
        {
            await Init();
            var widgets = await _db.Table<Widget>().ToListAsync();

            return widgets;
        }

        public static async Task UpdateWidget(int id, string name, string color, bool notificationStart, string notes)
        {
            await Init();

            var widgetQuerry = await _db.Table<Widget>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (widgetQuerry != null)
            {
                widgetQuerry.Name = name;
                widgetQuerry.Color = color;
                widgetQuerry.StartNotification = notificationStart;
                widgetQuerry.Notes = notes;

                await _db.UpdateAsync(widgetQuerry);
            }
        }

        #endregion

        #region Demo Data

        public static async Task LoadSampleData()
        {
            await Init();

            Gadget gadget = new Gadget()
            {
                Name = "Gadget 1",
                Color = "Teal"
            };

            await _db.InsertAsync(gadget);

            Widget widget = new Widget()
            {
                Name = "Widget 1",
                Color = "Blue",
                StartNotification = true,
                GadgetId = gadget.Id
            };

            await _db.InsertAsync(widget);
        }

        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Widget>();
            await _db.DropTableAsync<Gadget>();
            _db = null;
        }

        #endregion
    }
}
