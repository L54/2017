using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace TODO_BOM
{
	class Program
	{

		[DllImport("user32.dll")]
		static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
		[DllImport("user32.dll")]
		static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
		internal const uint SC_CLOSE = 0xF060;
		internal const uint MF_ENABLED = 0x00000000;
		internal const uint MF_GRAYED = 0x00000001;
		internal const uint MF_DISABLED = 0x00000002;
		internal const int MF_BYCOMMAND = 0x00000000;
		internal const int SC_MINIMIZE = 0xF020;

		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool AttachConsole(uint dwProcessId);
		[DllImport("kernel32.dll")]
		static extern IntPtr GetConsoleWindow();
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		static extern bool FreeConsole();

		[DllImport("user32.dll")]
		public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

		public static void EnableCloseButton(IntPtr window, bool bEnabled)
		{
			IntPtr hSystemMenu = GetSystemMenu(window, false);
			EnableMenuItem(hSystemMenu, SC_CLOSE, (MF_ENABLED | (bEnabled ? MF_ENABLED : MF_GRAYED)));
			DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MINIMIZE, MF_BYCOMMAND);
		}

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetWindowPos(
	   IntPtr hWnd,
	   IntPtr hWndInsertAfter,
	   int x,
	   int y,
	   int cx,
	   int cy,
	   int uFlags);

		private const int HWND_TOPMOST = -1;
		private const int SWP_NOMOVE = 0x0002;
		private const int SWP_NOSIZE = 0x0001;

		static /*ve-benel*/ void Main()
		{
			Console.Title = "2 0 1 7";

			NativeWindow win = new NativeWindow();
			EnableCloseButton(GetConsoleWindow(), false);

			IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
			SetWindowPos(hWnd,
				new IntPtr(HWND_TOPMOST),
				0, 0, 0, 0,
				SWP_NOMOVE | SWP_NOSIZE);

			int beat = 250;
			string
			s1 = @"
            `..--------.``                              `.---::::::--.``       
        .-::/osyhhhhys+::--`                        `--::oyhdmmmmdhs+::-.       
     `-::+hNMmhyssssydNMms/:-.                    `-:/ymMmhso++++oydNMh+::-`    
    .::omMh+:::::::::::/smMd/:-`                 -::hMms/::::::::::::+hMmo::.   
   -:/dMh/::::/osyys+:::::+mMs:-.              `-:/NMs:::::oyhhhhs+::::/dMh::.  
  -:/mMo::::sdho+//+sdd+::::dMh/::-.`       `.-::+NN+:::/hds/::::+yms::::yMh::` 
 `::hMs:::/my:::::::::/my::::mMMNho::-::::-::/ohNMMo:::+No::::::::::dd::::dMo:- 
 .::MN::::dh::::::::::::No:::sMMMMMMdhhdddhymMMMMMN::::my::::::::::::No:::sMd::`
 .:/Mm::::Ns::::::::::::my:::oMMMMMMMNNmmmNMMMMMMMN::::Ns::::::::::::ms:::oMd::.
 .::NM/:::ym:::::::::::+N/:::yMMMmyo/:::::::+sdMMMM+:::sm/::::::::::sN/:::hMy:- 
 `::sMh::::ym+::::::::sm+:::/NMd+:::::::::::::::sNMm::::oms/::::::+hd/:::+MN::. 
  .::hMh::::/ydhyssydds::::+NNo::::+hdhyyyhhs:::::dMm/::::ohdhhhhdy+::::sMN/:-  
   .::sMNs::::::/++/:::::/yMM+:::/mh/:::::::oms::::dMMy/::::::::::::::odMh::-   
    `-:/yNNho:::::::::/sdMMMh:::/No:::::::::::dy:::/MMMMmyo/::::::/ohNMh+:-.    
      `-::ohmMNmddddmMMMMMMMo:::sN::::::::::::oN-::-NMMMMMMMMNNNNMNdyo::-.      
        `.-:::/+ooooydMMMMMMs:::oN/:::::::::::ym::::NMMMMMMds+///:::--.`        
            ``......-::ohNMMm::::hm/:::::::::sN/:::oMMMmy+::-.`````             
                     `.-::hMMh::::omho+::/+ymy::::+NMN+::-.`                    
                        .::dMMd/::::/oyhhys+:::::yMMN+:-                        
                         .::sNMMdo::::::::::::/yNMMm/:-`                        
                          `::hMMMMMdysoo+osydNMMMMMy::                          
                           ::hMMMMMMMMNmmmNMMMMMMMMh::                          
                           ::hMMMNyo/:::::::/shNMMMh::                          
                          -::dMm+:::::::/:::::::sNMd::.                         
                         -:/mMs::::+hdhhyhddy/::::hMh::.                        
                        .::mMs:::/dd+:::::::omy::::hMy::`                       
                        ::oMd::::my:::::::::::dh::::NM/:-                       
                        ::yMy:::oN::::::::::::+M::::dMo:-                       
                        ::yMy:::/N/:::::::::::sN::::mMo:-                       
                        -:/MN::::ym/:::::::::oN+:::+MN::.                       
                        `::sMd::::+dho/:::+sdh/:::/NM+:-                        
                         .::sMm+::::/oyhhhso:::::sNN+:-`                        
                          `-:/dMdo::::::::::::/smMy::-                          
                            .-:/yNMmhso++ooyhmMms/:-`                           
                              .-::/oyhddmddhs+::-.`                             
                                 `..--::::---..`                       ",
								 s2= @"                                                                                
                                    ```....``                                   
                                .--::/+ooo+/::-.`                               
                             `-:/sdNNmddddmNMmho::-`                            
                           `-:+dMms+:::::::::+ymMh+:-`                          
                          .:/dMd/::::://+/::::::+mMh:-`                         
                         .:/NNo::::ohdyssshdh+::::sMm::`                        
                        `::mMo:::/my/:::::::/dd::::yMh:-                        
                        -:oMd::::Ns:::::::::::hd::::NM+:.                       
                        -:yMy:::oN::::::::::::+M::::hMo:.                       
                        -:sMh:::/N+:::::::::::sm::::mM+:.                       
                        `:/NN/:::omo:::::::::sm+:::+Mm::`                       
                         -:oMm/:::/hdy+///oydy/:::+NN+:.                        
                          -:oNNs:::::+ssyso/:::::yMN/:.                         
                           -:hMMNy/:::::::::::+yNMMh:-                          
                           -:hMMMMMNdyssssyhdNMMMMMh:-                          
                           -:hMMMMMMNmdhhhdmNMMMMMMh:-                          
                          .::dMMMds/:::::::::/smMMMd:-`                         
                         -:+NMMh/:::::/+++/:::::+dMMm/:.                        
                       `-:+NMN+::::sddysosyhdo::::sMMN/:.                       
                    `.-:/sNMM+:::+my::::::::/yd/:::sMMmo/:-.`                   
          ``.----::-:+ymMMMMd::::Ns:::::::::::sd::::NMMMMmy+::-:----.``         
       `.-:/oydmmNNmNMMMMMMMy:::oM/:::::::::::/M::::dMMMMMMMNmNNmmdyo/:-.       
     `-:/ymMmys+////+symMMMMh:::/No:::::::::::om::::mMMMMmhs+////+oymMms/:.`    
    .::yMms:::::::::::::/yNMN/:::oNs:::::::::sm/:::oMMNy/:::::::::::::smNy:-.   
   .:+NNs::::/oyhhhhyo:::::yMm/:::/ydho+++shds::::+NMy:::::oyhhhhyo/::::sMN+:-  
  .:/NN+:::/hdo/::::/smy::::sMNs:::::/osso+/::::/hMMo::::yds/::::/odh/:::+NN/:. 
  -:dMs:::+No::::::::::ym::::hMMNy+:::::::::::ohMMMy::::my::::::::::sN/:::sMh:- 
 `::MN::::dh::::::::::::my:::+MMMMMMmhyysyydmMMMMMM+:::yd::::::::::::hh:::/MN::`
 `::MN::::dy::::::::::::my:::+MMMMMMMNMMMMNMMMMMMMM+:::yd::::::::::::hd::::MN::`
  -:mMo:::oN+::::::::::oN/:::yMMMMmy+:::::::+ymMMMMy:::/No::::::::::+N+:::oMd:- 
  .:+MN/:::+my/::::::+hd/:::+MMho/:-.` ``` `.-:/sdMM+:::/dh+::::::+hm+:::/NN/:. 
   -:oNN+::::+ydhhhhhy/::::oNN/:-`            `.-:+NNo::::+yhhhhhhy+::::+NN+:-  
    .:/dMd+::::::::::::::omMh::.                 .:/hMdo::::::::::::::+dMd/:.   
     `-:+dNNho+:::::/+shNNh/:-`                   `-:/hNNhs+/::::/+ohNNh+:-`    
       `-::oydmNNNNNNmdy+:-.`                       `.::+ydmNNNNNNmdy+::.`      
          `.---::::::---.`                             `.--::::::::--.`         
                ````                                         ````         ";
			Console.SetWindowSize(81, 38);
			Console.SetBufferSize(81, 38);
			while (!File.Exists("stop") && DateTime.Today.Year.ToString().Equals("2017"))
			{
				beat = 250;
				Console.Clear();
				Console.Write(s1);
				Console.Beep(659, beat);
				Console.Beep(659, beat);
				Console.Beep(659, beat / 2);
				Console.Beep(698, (int)(beat * 1.5));
				Console.Clear();
				Console.Write(s2);
				Console.Beep(698, beat);
				Console.Beep(659, beat / 2);
				Console.Beep(587, beat);
				Console.Beep(587, beat / 2);
				Console.Clear();
				Console.Write(s1);
				Console.Beep(587, beat);
				Console.Beep(698, beat / 2);
				Console.Beep(659, (int)(beat * 1.5));
				Console.Beep(494, beat);
				Console.Clear();
				Console.Write(s2);

				Thread.Sleep(beat);

				Console.Beep(659, beat);
				Console.Beep(659, beat);
				Console.Beep(659, beat / 2);
				Console.Beep(698, (int)(beat * 1.5));
				Console.Clear();
				Console.Write(s1);
				Console.Beep(698, beat / 2); Console.Beep(698, beat / 2);
				Console.Beep(659, beat / 2);
				Console.Beep(587, beat);
				Console.Clear();
				Console.Write(s2);
				Console.Beep(587, beat / 2);
				Console.Beep(659, beat);
				Console.Beep(698, beat / 2);
				Console.Beep(659, beat);
				Console.Clear();
				Console.Write(s1);

				Thread.Sleep(beat * 3);

				Console.Beep(659, beat);
				Console.Beep(659, beat);
				Console.Beep(659, beat / 2);
				Console.Clear();
				Console.Write(s2);
				Console.Beep(698, (int)(beat * 1.5));
				Console.Beep(698, beat);
				Console.Beep(698, beat / 2);
				Console.Beep(784, beat / 2);
				Console.Beep(698, beat / 2);
				Console.Clear();
				Console.Write(s1);
				Console.Beep(659, beat);
				Console.Beep(698, beat / 2);
				Console.Beep(659, beat);
				Console.Beep(494, beat);
				Console.Clear();
				Console.Write(s2);

				Thread.Sleep((int)(beat * 1.5));

				//אצלנו אומרים זה בסדר
				Console.Beep(659, beat / 2);
				Console.Beep(784, beat);
				Console.Beep(659, beat / 2);
				Console.Beep(659, beat / 2);
				Console.Beep(659, (int)(beat * 1.5));
				Console.Beep(659, beat / 2);
				Console.Beep(698, beat / 2);
				Console.Beep(698, (int)(beat * 1.5));

				Console.Clear();
				Console.Write(s1);
				Console.Beep(698, beat);
				Console.Beep(659, beat / 2);
				Console.Beep(587, beat);
				Console.Beep(587, beat / 2);
				Console.Beep(587, beat);
				Console.Beep(587, beat);

				beat = 125;
				Console.Clear();
				Console.Write(s2);
				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Clear();
				Console.Write(s1);
				Console.Beep(659, beat * 2);

				Thread.Sleep(beat * 3);

				Console.Beep(587, beat);
				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Clear();
				Console.Write(s2);
				Console.Beep(784, beat);
				Console.Beep(659, beat * 2);

				Thread.Sleep(beat * 3);

				Console.Beep(784, beat / 2);
				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Clear();
				Console.Write(s1);
				Console.Beep(784, beat);
				Console.Beep(659, beat);

				Thread.Sleep(beat * 3);

				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Beep(659, beat * 2);
				Console.Clear();
				Console.Write(s2);

				Console.Beep(784, beat);
				Console.Beep(784, beat);
				Console.Beep(659, beat * 2);
				Console.Beep(659, beat);
				Console.Clear();
				Console.Write(s1);

				Thread.Sleep(beat * 6);
			}
		}
	}
}