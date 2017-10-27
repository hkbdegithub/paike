using System.ComponentModel;

namespace Demo
{
    /// <summary>
    /// 教师授课计划类
    /// </summary>
    internal class TeachingProject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public string Grade { get; set; }   // 年级
        public string Major { get; set; }   // 专业
        public string CourseName { get; set; }  // 课程名称
        public string TotalHours { get; set; }  // 总学时
        public string WeekOfTeaching { get; set; }  // 教学周
        public string NumberOfPeople { get; set; }  // 人数
        public string Teacher { get; set; } // 授课教师
        public string UseComputerRoom { get; set; } // 使用机房学时
        public string UseClassroomHours { get; set; }   // 使用教室学时
        public string UsePcClassroom { get; set; }  // 使用笔记本教师学时
        public string TeacherSchedulingTime { get; set; }   // 教师排课时间要求
        public string TeacherCurriculumRequirements { get; set; }   // 教师排课课程要求
        public string CourseNameAndTeacher { get; set; }    // 课程名称 + 授课教师
    }
}
