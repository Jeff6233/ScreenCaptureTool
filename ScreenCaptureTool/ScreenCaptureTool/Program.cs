using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenCaptureTool
{
    internal static class Program
    {
        static Mutex mutex = new Mutex(true, "{ScreenCaptureTool}");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                //Application.Run(new frmMain());

                // 创建一个自定义的 ApplicationContext，用于隐藏主窗体
                MyApplicationContext context = new MyApplicationContext();

                // 使用 Application.Run 方法启动应用程序
                Application.Run(context);

                // 释放互斥体
                mutex.ReleaseMutex();
            }
        }

        // 自定义的 ApplicationContext 类
        public class MyApplicationContext : ApplicationContext
        {
            public MyApplicationContext()
            {
                // 在这里添加你希望在后台运行的逻辑

                // 例如，执行一些后台任务或服务
                // 请确保在这里不会显示任何图形界面
                frmMain f=new frmMain();
                f.Show();
                f.Hide();
            }

            // 重写 Exit 方法，可以在退出应用程序时执行一些逻辑
            protected override void ExitThreadCore()
            {
                // 在退出前释放资源等
                // ...

                base.ExitThreadCore();
            }
        }
    }
}
