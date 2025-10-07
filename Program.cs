using System;
using Terminal.Gui;
using System.IO;

namespace HelloWorldApp
{
    class Program
    {
    
        static string selectedFile;
    
        static void Main(string[] args)
        {
        	if (args.Length == 1)
        	{
        		Edit(args[0]);
        		return;
        	}
            //Console.WriteLine("Hello, World!");
            
            Console.WriteLine("Welcome to Winderhub!");
             
            Menu();
            
        }
        
        static void Exit()
        {
            Application.Shutdown();
            Environment.Exit(0);
        }
        
        static void Menu()
        {       
            Console.WriteLine("Choose an option to continue:");
            
            Console.WriteLine("1. Edit A File");
            Console.WriteLine("2. Edit a Zig Project");
            Console.WriteLine("3. Exit");
            
            Console.Write("Your Choice:");
            string option = Console.ReadLine();
            
            if(option == "1")
            {
                SelectFile();
                
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    Edit(selectedFile);
                }
                
                OpenFileMenu();

            }
            
            if(option == "2")
            {
                Console.WriteLine("This App doesent support this feature yet.");
            }
            
            if (option == "3")
            {
                Exit();
            }
        }
        
        
        static void OpenFileMenu()
        {
            Console.WriteLine("Choose an option");
            
            Console.WriteLine("1. Edit Another File");
            Console.WriteLine("2. Back to main Menu");
            Console.WriteLine("3. Exit");
            
            Console.Write("Your Choice:");
            string option = Console.ReadLine();
            
            if (option == "1")
            {
                SelectFile();
                
                if (!string.IsNullOrEmpty(selectedFile))
                {
                    Edit(selectedFile);
                }
            }
            
            if (option == "2")
            {
                Menu();
            }
            
            if (option == "3")
            {
                Exit();
            }
        }
        
        
        static void OpenZigProj()
        {
            Application.Init();

            var dialog = new OpenDialog("Open Zig Project", "Select a folder")
            {
                AllowsMultipleSelection = false,
                CanChooseDirectories = true,
                CanChooseFiles = false
            };

            Application.Run(dialog);

            if (dialog.FilePaths.Count > 0)
            {
                string selectedFolder = dialog.FilePaths[0];
                Console.WriteLine($"Selected Zig project folder: {selectedFolder}");

                // You can now scan for build.zig, src/, etc.
                // Example: if (File.Exists(Path.Combine(selectedFolder, "build.zig"))) { ... }
            }

            Application.Shutdown();
        }

        
        

        static void SelectFile()
        {
            Application.Init();

            var dialog = new OpenDialog("Select a File", "Choose one file")
            {
                    AllowsMultipleSelection = false,
                    CanChooseDirectories = false
            };

            Application.Run(dialog);

            if (dialog.FilePaths.Count > 0)
            {
                selectedFile = dialog.FilePaths[0];
                Console.WriteLine($"Selected file: {selectedFile}");
            }

            Application.Shutdown();
        }
        
        static void Edit(string file)
        {
            Application.Init();

            var top = Application.Top;
            var win = new Window($"Editing: {Path.GetFileName(file)}")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);

            var textView = new TextView()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
        win.Add(textView);

        // Load file contents
        if (File.Exists(file))
        {
            textView.Text = File.ReadAllText(file);
        }
        else
        {
            textView.Text = "";
        }

        // Save on Ctrl+S
        top.Add(new MenuBar(new MenuBarItem[]
        {
            new MenuBarItem("_File", new MenuItem[]
            {
                new MenuItem("_Save", "Ctrl+S", () =>
                {
                    File.WriteAllText(file, textView.Text.ToString());
                    MessageBox.Query(40, 7, "Saved", "File saved successfully.", "OK");
                }),
                    new MenuItem("_Quit", "Ctrl+Q", () => Application.RequestStop())
                })
            }));

            Application.Run();
            Application.Shutdown();
        }

        
    }
}
