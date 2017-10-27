using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private readonly List<TeachingProject> _teachingProjectsList = new List<TeachingProject>(); // 授课计划 List
        private readonly List<ClassroomInfo> _classroomInfosList = new List<ClassroomInfo>(); // 教室信息 List
        private readonly List<ClassInfo> _classInfosList = new List<ClassInfo>(); // 班级信息 List
        public MainWindow()
        {
            InitializeComponent();
            // 连接数据库获取课程信息
            ConnectDbForTeachingProject();
            // 连接数据库获取教室信息
            ConnectDbForClassroomInfo();
            // 连接数据库获取班级信息表数据
            ConnectDbForClassInfo();
            // 刷新授课计划列表
            RefreshClassListBox();
            // 刷新教室信息列表
            RefreshClassroomListBox();
            //PrintInfo();
            ClearTimetables<Label>(TimetablesGrid);
            //清空课程表内容
        }

        #region 主窗口操作代码

            /// <summary>
            /// 关闭按钮
            /// </summary>
            private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 放大缩小按钮
        /// </summary>
        private int _i = 1;
        private void MaximizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (_i == 1)
            {
                WindowState = WindowState.Maximized;
                _i = 0;
            }
            else if (_i == 0)
            {
                WindowState = WindowState.Normal;
                _i = 1;
            }
        }

        /// <summary>
        /// 最小化按钮
        /// </summary>
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion

        /// <summary>
        /// 连接数据库获取教师授课计划表数据
        /// </summary>
        public void ConnectDbForTeachingProject()
        {
            // 获取数据库路径
            var path0 = Environment.CurrentDirectory;
            var path1 = path0.Substring(0, path0.LastIndexOf("\\", StringComparison.Ordinal));
            var path2 = path1.Substring(0, path1.LastIndexOf("\\", StringComparison.Ordinal));
            var dbPath = path2 + "\\DB\\course_scheduling_system.db";
            #region
            using (var conn = new SQLiteConnection())
            {
                var connsb = new SQLiteConnectionStringBuilder { DataSource = dbPath };
                conn.ConnectionString = connsb.ToString();
                conn.Open();
                using (new SQLiteCommand(conn))
                {
                    const string sql = "SELECT * FROM tb_teachingproject";
                    var command = new SQLiteCommand(sql, conn);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var tp = new TeachingProject
                        {
                            Grade = reader["grade"].ToString(),
                            Major = reader["major"].ToString(),
                            CourseName = reader["course_name"].ToString(),
                            TotalHours = reader["total_hours"].ToString(),
                            WeekOfTeaching = reader["week_of_teaching"].ToString(),
                            NumberOfPeople = reader["number_of_people"].ToString(),
                            Teacher = reader["teacher"].ToString(),
                            UseComputerRoom = reader["use_computer_room"].ToString(),
                            UseClassroomHours = reader["use_classroom_hours"].ToString(),
                            UsePcClassroom = reader["use_pc_classroom"].ToString(),
                            TeacherSchedulingTime = reader["teacher_scheduling_time"].ToString(),
                            TeacherCurriculumRequirements = reader["teacher_curriculum_requirements"].ToString(),
                            CourseNameAndTeacher = reader["course_name"] + "\t" + reader["teacher"]
                        };
                        _teachingProjectsList.Add(tp);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 连接数据库获取教室信息表数据
        /// </summary>
        public void ConnectDbForClassroomInfo()
        {
            // 获取数据库路径
            var path0 = Environment.CurrentDirectory;
            var path1 = path0.Substring(0, path0.LastIndexOf("\\", StringComparison.Ordinal));
            var path2 = path1.Substring(0, path1.LastIndexOf("\\", StringComparison.Ordinal));
            var dbPath = path2 + "\\DB\\course_scheduling_system.db";
            #region
            using (var conn = new SQLiteConnection())
            {
                var connsb = new SQLiteConnectionStringBuilder { DataSource = dbPath };
                conn.ConnectionString = connsb.ToString();
                conn.Open();
                using (new SQLiteCommand(conn))
                {
                    const string sql = "SELECT * FROM tb_classroominfo";
                    var command = new SQLiteCommand(sql, conn);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var ci = new ClassroomInfo
                        {
                            ClassroomNumber = reader["classroom_number"].ToString(),
                            ClassroomGalleryful = reader["classroom_galleryful"].ToString(),
                            ClassroomType = reader["classroom_type"].ToString(),
                            DetInfo = reader["classroom_number"] + "\t" + reader["classroom_type"]
                        };
                        _classroomInfosList.Add(ci);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 连接数据库获取班级信息表数据
        /// </summary>
        public void ConnectDbForClassInfo()
        {
            // 获取数据库路径
            var path0 = Environment.CurrentDirectory;
            var path1 = path0.Substring(0, path0.LastIndexOf("\\", StringComparison.Ordinal));
            var path2 = path1.Substring(0, path1.LastIndexOf("\\", StringComparison.Ordinal));
            var dbPath = path2 + "\\DB\\course_scheduling_system.db";
            #region
            using (var conn = new SQLiteConnection())
            {
                var connsb = new SQLiteConnectionStringBuilder { DataSource = dbPath };
                conn.ConnectionString = connsb.ToString();
                conn.Open();
                using (new SQLiteCommand(conn))
                {
                    const string sql = "SELECT * FROM tb_classinfo";
                    var command = new SQLiteCommand(sql, conn);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var ci = new ClassInfo
                        {
                            ClassName = reader["class_name"].ToString(),
                            Grade = reader["grade"].ToString(),
                            Major = reader["major"].ToString(),
                            ClassNameAndGrade = reader["grade"].ToString() + reader["class_name"].ToString()
                        };
                        _classInfosList.Add(ci);
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 测试输出
        /// </summary>
        public void PrintInfo()
        {
            foreach (var tp in _teachingProjectsList)
            {
                Console.WriteLine(@"{0}	{1}	{2}	{3}	{4}	{5}	{6}	{7}	{8}	{9} {10}    {11}", tp.Grade, tp.Major, tp.CourseName, tp.TotalHours, tp.WeekOfTeaching,
                    tp.NumberOfPeople, tp.Teacher, tp.UseComputerRoom, tp.UseClassroomHours, tp.UsePcClassroom, tp.TeacherSchedulingTime, tp.TeacherCurriculumRequirements);
            }
        }

        /// <summary>
        /// 刷新 授课计划 列表
        /// </summary>
        private void RefreshClassListBox()
        {
            ClassListBox.ItemsSource = null;
            ClassListBox.ItemsSource = _teachingProjectsList;
            ClassListBox.Items.Refresh();
        }

        /// <summary>
        /// 刷新 教室信息 列表
        /// </summary>
        private void RefreshClassroomListBox()
        {
            ClassroomListBox.ItemsSource = null;
            ClassroomListBox.ItemsSource = _classroomInfosList;
            ClassroomListBox.Items.Refresh();
        }

        /// <summary>
        /// ClassListBox鼠标点击事件
        /// </summary>
        private void ClassListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTp = ClassListBox.SelectedValue as TeachingProject;
            // 清空之前留下的班级
            ClassInfoListBox.Items.Clear();
            if (selectedTp == null)
            {
                ClassInforLable.Text = "课程介绍";
                return;
            }
            //Console.WriteLine(selectedTp.CourseNameAndTeacher);
            
            var tp = selectedTp;

            ushort position = 0;
            // 显示该门课程上课的班级
            foreach (var cil in _classInfosList)
            {
                if ((cil.Grade + cil.Major) == (tp.Grade + tp.Major))
                {
                    cil.position = position;
                    ClassInfoListBox.Items.Add(cil);
                }
                position ++;
            }

            if (tp.UseComputerRoom == "")
            {
                tp.UseComputerRoom = "0";
            }
            if (tp.UseClassroomHours == "")
            {
                tp.UseClassroomHours = "0";
            }
            if (tp.UsePcClassroom == "")
            {
                tp.UsePcClassroom = "0";
            }
            var info = "总学时：" + tp.TotalHours + "\n" +
                       "教学周：" + tp.WeekOfTeaching + "\n" +
                       "人数：" + tp.NumberOfPeople + "\n" +
                       "授课教师：" + tp.Teacher + "\n" +
                       "使用机房学时：" + tp.UseComputerRoom + "\n" +
                       "使用教室学时：" + tp.UseClassroomHours + "\n" +
                       "使用笔记本教师学时：" + tp.UsePcClassroom + "\n" +
                       "教师排课时间要求：" + tp.TeacherSchedulingTime + "\n" +
                       "教师排课课程要求：" + tp.TeacherCurriculumRequirements;
            ClassInforLable.Text = info;

            #region 注释掉的备用代码
            //foreach (var tp in _teachingProjectsList)
            //{
            //    if (tp.CourseNameAndTeacher != selectedTp.CourseNameAndTeacher) continue;

            //    // 显示该门课程上课的班级
            //    if (tp.TotalHours == selectedTp.TotalHours && tp.WeekOfTeaching == selectedTp.WeekOfTeaching)
            //    {
            //        foreach (var cil in _classInfosList)
            //        {
            //            if ((cil.Grade + cil.Major) == (tp.Grade + tp.Major))
            //            {
            //                ClassInfoListBox.Items.Add(cil.ClassName);
            //            }
            //        }
            //    }

            //    if (tp.UseComputerRoom == "")
            //    {
            //        tp.UseComputerRoom = "0";
            //    }
            //    if (tp.UseClassroomHours == "")
            //    {
            //        tp.UseClassroomHours = "0";
            //    }
            //    if (tp.UsePcClassroom == "")
            //    {
            //        tp.UsePcClassroom = "0";
            //    }
            //    var info = "总学时：" + tp.TotalHours + "\n" +
            //               "教学周：" + tp.WeekOfTeaching + "\n" +
            //               "人数：" + tp.NumberOfPeople + "\n" +
            //               "授课教师：" + tp.Teacher + "\n" +
            //               "使用机房学时：" + tp.UseComputerRoom + "\n" +
            //               "使用教室学时：" + tp.UseClassroomHours + "\n" +
            //               "使用笔记本教师学时：" + tp.UsePcClassroom + "\n" +
            //               "教师排课时间要求：" + tp.TeacherSchedulingTime + "\n" +
            //               "教师排课课程要求：" + tp.TeacherCurriculumRequirements;
            //    ClassInforLable.Text = info;
            //}
            #endregion
        }

        /// <summary>
        /// 添加课程到课程表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            //ClassInfoListBox.SelectedItems
            //ClassListBox.SelectedItem
            //ClassroomListBox.SelectedItem
            if (ClassroomListBox.SelectedItem is null || ClassListBox.SelectedItem is null || ClassInfoListBox.SelectedItem is null) return;
            Button button = sender as Button;
            ClassroomInfo classroomInfo = ClassroomListBox.SelectedValue as ClassroomInfo;
            TeachingProject tp = ClassListBox.SelectedValue as TeachingProject;

            Course course = new Course()
            {
                CourseName = tp.CourseName,
                Teacher = tp.Teacher,
                Classroom = classroomInfo.ClassroomNumber
            };

            foreach (var item in ClassInfoListBox.SelectedItems)
            {
                ClassInfo classInfo = (ClassInfo)item;

                if (classInfo.Timetables is null)
                {
                    classInfo.Timetables = new Dictionary<Button, Course>();
                }

                if (classInfo.Timetables.ContainsKey(button))
                {
                    if (ClassInfoListBox.SelectedItems.Count > 1)
                    {
                        MessageBox.Show("已排课", "警告");
                        return;
                    }
                    else
                    {
                        classInfo.Timetables[button] = course;//修改待定
                    }
                }
                else
                {
                    classInfo.Timetables.Add(button, course);
                }                
            }

            Grid g = button.Parent as Grid;
            Label l = g.Children[0] as Label;
            l.Content = course.CourseName + "\n" +
                course.Classroom + "\n" +
                course.Teacher + "\n";
        }

        /// <summary>
        /// 根据班级修改主界面课程表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClassInfoListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SendButtonEnable<Button>(TimetablesGrid);
            ClassInfo classInfo = null;
            ushort tempPosition = 65535;
            foreach (var item in ClassInfoListBox.SelectedItems)
            {                
                ClassInfo cli = item as ClassInfo;
                if (cli.Timetables != null && cli.Timetables.Count > 0)
                {
                    foreach (var key in cli.Timetables.Keys)
                    {
                        key.IsEnabled = false;
                    }
                }
                if (cli.position < tempPosition)
                {
                    tempPosition = cli.position;
                    classInfo = cli;
                }
            }
            //ClassInfo classInfo = ClassInfoListBox.SelectedItem as ClassInfo;

            ClearTimetables<Label>(TimetablesGrid);

            if (classInfo is null || classInfo.Timetables is null || classInfo.Timetables.Count == 0) return;

            foreach (var key in classInfo.Timetables.Keys)
            {
                Button button = key as Button;
                Grid g = button.Parent as Grid;
                Label l = g.Children[0] as Label;
                l.Content = classInfo.Timetables[button].CourseName + "\n" +
                    classInfo.Timetables[button].Classroom + "\n" +
                    classInfo.Timetables[button].Teacher + "\n";
            }
        }

        /// <summary>
        /// 清空课程表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        private void ClearTimetables<T>(DependencyObject obj) where T : FrameworkElement
        {
            if (obj is T)
            {
                ClearCourse(obj);
            }
            if (obj is ListBox) return;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                ClearTimetables<T>(VisualTreeHelper.GetChild(obj, i));
            }
        }

        /// <summary>
        /// 清空一个课
        /// </summary>
        /// <param name="obj"></param>
        private void ClearCourse(DependencyObject obj)
        {
            var label = obj as Label;
            label.Content = "请添加课程";
        }

        /// <summary>
        /// 回复按钮
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        private void SendButtonEnable<T>(DependencyObject obj) where T : FrameworkElement
        {
            if (obj is T)
            {
                Button button = obj as Button;
                button.IsEnabled = true;
            }
            if (obj is ListBox) return;
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                SendButtonEnable<T>(VisualTreeHelper.GetChild(obj, i));
            }
        }
        ///// <summary>
        ///// 通过视觉树 更改 Label 样式
        ///// </summary>
        //public void ChangeVisualTree<T>(DependencyObject obj) where T : FrameworkElement
        //{
        //    if (obj is T)
        //    {
        //        if (typeof(T).Name == "Label")
        //        {
        //            var label = obj as System.Windows.Controls.Label;
        //            if (label != null)
        //            {
        //                label.Height = 40;
        //                label.Visibility = Visibility.Visible;
        //            }
        //        }
        //    }
        //    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
        //    {
        //        ChangeVisualTree<T>(VisualTreeHelper.GetChild(obj, i));
        //    }
        //}
    }
}
