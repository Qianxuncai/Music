using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace 控制台听歌
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("欢迎使用YERT听歌服务！！！" + "\n");
            while (true)
            {
                Console.Write("YERT: !>");
                string str = Console.ReadLine();
                //命令输出相应的结果

                string Ye = puanduan1(str); ;
                if (Ye == "false")
                {
                    Console.WriteLine("");
                }
            }
        }
        static string puanduan1(string str)//从上面引用puanduan1,赋值给xuaxiang1
        {
            if (str == "music")
            {
                duqu();
                return "true";
            }
            else if (str == "播放歌曲")
            {
                bofang();
                return "true";
            }
            else
            {
                return "false";
            }
        }

        static void duqu()
        {
            string folderPath = @"本地歌曲/";
            string[] fileNames = Directory.GetFileSystemEntries(folderPath, "*", SearchOption.AllDirectories);

            foreach (string fileName in fileNames)
            {
                Console.WriteLine(Path.GetFileName(fileName));
                Console.WriteLine();
            }
        }

        static void bofang()
        {
            bool isPlaying = true;
            List<string> songList = GetSongList(@"./本地歌曲/");

            while (isPlaying)
            {
                Console.WriteLine();
                Console.WriteLine("请选择要播放的歌曲：");

                for (int i = 0; i < songList.Count; i++) {
                    Console.WriteLine("{0}. {1}", (i + 1).ToString(), Path.GetFileName(songList[i]).ToString()); 
                }
                Console.WriteLine("{0}. 退出", songList.Count + 1); 
                string choice = Console.ReadLine(); 
                int songIndex; 
                if(int.TryParse(choice, out songIndex))
                {
                    if (songIndex >= 1 && songIndex <= songList.Count)
                    {
                        PlaySong(songList[songIndex - 1]);
                    }
                    else if (songIndex == songList.Count + 1) { isPlaying = false; }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("无效的选择，请重新输入！");
                    }
                }
            }
        }

        static void PlaySong(string filePath)
        {
            try
            {
                SoundPlayer player = new SoundPlayer(filePath);
                player.Play();

                Console.WriteLine("正在播放音乐，请按空格键暂停...");

                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Spacebar)
                        {
                            player.Stop();
                            Console.WriteLine("音乐已暂停");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("播放音乐时出现错误: " + ex.Message);
            }
        }

        static List<string> GetSongList(string directoryPath)
        {
            List<string> songList = new List<string>();

            try
            {
                string[] fileEntries = Directory.GetFiles(directoryPath); 
                foreach(string filePath in fileEntries) {
                    songList.Add(filePath); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取本地歌曲列表时出现错误: " + ex.Message);
            }

            return songList;
        }
    }
}