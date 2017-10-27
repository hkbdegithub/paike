namespace Demo
{
    /// <summary>
    /// 班级信息
    /// </summary>
    internal class ClassInfo
    {
        public string ClassName { get; set; } // 班级编号

        public string Grade { get; set; } // 年级

        public string ClassNameAndGrade { get; set; } // 年级 + 班级编号

        public string Major { get; set; } // 专业

        public System.Collections.Generic.Dictionary<System.Windows.Controls.Button, Course> Timetables { get; set; }

        public ushort position; //ClassInfoListBox里的相对位置
    }
}
