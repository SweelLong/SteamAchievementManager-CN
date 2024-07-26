/* Copyright (c) 2019 Rick (rick 'at' gibbed 'dot' us)
 * 
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 * 
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 * 
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

using System;
using System.Windows.Forms;

namespace SAM.Picker
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            MessageBox.Show("欢迎使用Steam成就管理器7.0汉化版\n由SweelLong提供汉化\n依照LICENSE此项目开源", "提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (API.Steam.GetInstallPath() == Application.StartupPath)
            {
                MessageBox.Show(
                    "此工具无法从Steam文件夹运行。",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            using (var client = new API.Client())
            {
                try
                {
                    client.Initialize(0);
                }
                catch (API.ClientInitializeException e)
                {
                    if (string.IsNullOrEmpty(e.Message) == false)
                    {
                        MessageBox.Show(
                            "Steam没有运行。请启动Steam，然后再次运行此工具。\n\n" +
                            "(" + e.Message + ")",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Steam没有运行。请启动Steam，然后再次运行此工具。",
                            "错误 ",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    return;
                }
                catch (DllNotFoundException)
                {
                    MessageBox.Show(
                        "您造成了异常错误！",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new GamePicker(client));
            }
        }
    }
}
