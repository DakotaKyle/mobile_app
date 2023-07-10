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

        #region Init Method

        static async Task Init()
        {
            if (_db != null) // Don't create the database if it already exists.
            {
                return;
            }

            //get an absolute path to the database file.
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Gadgets.db");

            _db = new SQLiteAsyncConnection(databasePath);

            await _db.CreateTableAsync<Course>();
            await _db.CreateTableAsync<Assessment>();
        }

        #endregion

        #region Course methods

        public static async Task AddCourse(string name, string color, DateTime creationDate)
        {
            await Init();

            var course = new Course()
            {
                Name = name,
                Color = color,
                CreationDate = creationDate
            };

            await _db.InsertAsync(course);
        }

        public static async Task RemoveCourse(int id)
        {
            await Init();
            await _db.DeleteAsync<Course>(id);
        }

        public static async Task<IEnumerable<Course>> GetCourses()
        {
            await Init();

            var courses = await _db.Table<Course>().ToListAsync();
            return courses;
        }

        public static async Task UpdateCourse(int id, string name, string color, DateTime creationDate)
        {
            await Init();

            var courseQuery = await _db.Table<Course>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (courseQuery != null)
            {
                courseQuery.Name = name;
                courseQuery.Color = color;
                courseQuery.CreationDate = creationDate;

                await _db.UpdateAsync(courseQuery);
            }
        }

        #endregion

        #region Assessment methods

        public static async Task AddAssessment(int courseId, string name, string color, DateTime creationDate, bool notificationStart, string notes)
        {
            await Init();
            var assessment = new Assessment()
            {
                CourseId = courseId,
                Name = name,
                Color = color,
                CreationDate = creationDate,
                StartNotification = notificationStart,
                Notes = notes
            };

            await _db.InsertAsync(assessment);
        }

        public static async Task RemoveAssessment(int id)
        {
            await Init();
            await _db.DeleteAsync<Assessment>(id);
        }

        public static async Task<IEnumerable<Assessment>> GetAssessments(int courseId)
        {
            await Init();

            var courses = await _db.Table<Assessment>().Where(i => i.CourseId == courseId).ToListAsync();

            return courses;
        }

        public static async Task<IEnumerable<Assessment>> GetAssessments()
        {
            await Init();
            var assessments = await _db.Table<Assessment>().ToListAsync();

            return assessments;
        }

        public static async Task UpdateAssessment(int id, string name, string color, DateTime creationDate, bool notificationStart, string notes)
        {
            await Init();

            var assessmentQuerry = await _db.Table<Assessment>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (assessmentQuerry != null)
            {
                assessmentQuerry.Name = name;
                assessmentQuerry.Color = color;
                assessmentQuerry.CreationDate = creationDate;
                assessmentQuerry.StartNotification = notificationStart;
                assessmentQuerry.Notes = notes;

                await _db.UpdateAsync(assessmentQuerry);
            }
        }

        #endregion

        #region Demo Data

        public static async Task LoadSampleData()
        {
            await Init();

            Course course = new Course()
            {
                Name = "Gadget 1",
                Color = "Teal",
                CreationDate = DateTime.Today.Date
            };

            await _db.InsertAsync(course);

            Assessment assessment = new Assessment()
            {
                Name = "Widget 1",
                Color = "Blue",
                CreationDate = DateTime.Today.Date,
                StartNotification = true,
                CourseId = course.Id
            };

            await _db.InsertAsync(assessment);
        }

        public static async Task ClearSampleData()
        {
            await Init();

            await _db.DropTableAsync<Assessment>();
            await _db.DropTableAsync<Course>();
            _db = null;
        }

        #endregion
    }
}
