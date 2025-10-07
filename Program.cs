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
            //Console.WriteLine("Hello, World!");
               
            SelectFile();
            Edit(selectedFile);


			
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
