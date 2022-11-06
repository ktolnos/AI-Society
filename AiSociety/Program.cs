using System;
using AiSociety;
using Eto.Forms;
using Eto.Drawing;
using Eto.Forms.Controls.SkiaSharp.Shared;
using SkiaSharp;

public class MyForm : Form
{
    private Drawable _drawable;
    
    public MyForm ()
    {
        ClientSize = new Size(800, 400);
        Menu = new MenuBar
        {
            Items =
            {
                new ButtonMenuItem
                {
                    Text = "&File",
                    Items =
                    {
                        // you can add commands or menu items
                        new MyCommand(),
                        // another menu item, not based off a Command
                        new ButtonMenuItem {Text = "Click Me, MenuItem"}
                    }
                }
            },
            QuitItem = new Command((sender, e) => Application.Instance.Quit())
            {
                MenuText = "Quit",
                Shortcut = Application.Instance.CommonModifier | Keys.Q
            }
        };
        var aboutItem = new ButtonMenuItem { Text = "About..." };
        aboutItem.Click += (sender, e) =>
        {
            var dlg = new Dialog
            { 
                Content = new Label { Text = "About my app..." }, 
                ClientSize = new Size(800, 400)
            };
            dlg.ShowModal(this);
        };
        Menu.AboutItem = aboutItem;
        ToolBar = new ToolBar
        {
            Items =
            { 
                new MyCommand(),
                new SeparatorToolItem(),
                new ButtonToolItem { Text = "Click Me, ToolItem" }
            }
        };
        var layout = new TableLayout
        {
            Spacing = new Size(5, 5),
            Padding = new Padding(10, 10, 10, 10)
        };
        // space between each cell
        // space around the table's sides
        layout.Rows.Add(new TableRow(
            new TableCell(new Label { Text = "First Column" }, true),
            new TableCell(new Label { Text = "Second Column" }, true),
            new Label { Text = "Third Column" }
        ));

        _drawable = new Drawable(true) {Size = new Size(1000, 1000)};
        var scroll = new Scrollable {Content = _drawable};
        Graphics graphics = null;
        _drawable.Paint += (sender, args) => {
            if (graphics == null)
            {
                graphics = _drawable.CreateGraphics();
            }
            //Console.WriteLine(graphics.ClipBounds.ToString());
            var handler = GraphicsRepaint;
            handler?.Invoke(this, new GraphicsRepaintEventArgs{Graphics = graphics});
        };
        
        //graphics.DrawRectangle(new Color(1, 0, 0), 10, 10, 100, 50);
        layout.Rows.Add(new TableRow(
                scroll,
                new CheckBox {Text = "A checkbox"}
            ) {ScaleHeight = true});
        this.Content = layout;

        
    }
    
    public event EventHandler<GraphicsRepaintEventArgs> GraphicsRepaint;
    
    public class GraphicsRepaintEventArgs : EventArgs
    {
        public Graphics Graphics { get; set; }
    }

	
    [STAThread]
    static void Main()
    {
        var application = new Application();
        var form = new MyForm();
        World world = null;
        form.GraphicsRepaint += (sender, args) =>
        {
            if (world == null)
            {
               // world = new World(1000, 1000, 2000, args.Graphics);
            }
            //world.Draw();
        };
         /*  
        var skControl = new SKControl(){Height = 1000, Width = 1000};
        skControl.PaintSurfaceAction += skSurface =>
        {
            SKCanvas canvas = skSurface.Canvas;
            
            // clear the canvas / fill with white
            canvas.Clear (SKColors.White);

            // set up drawing tools
            using (var paint = new SKPaint ()) {
                paint.IsAntialias = true;
                paint.Color = new SKColor (0x2c, 0x3e, 0x50);
                paint.StrokeCap = SKStrokeCap.Round;

                // create the Xamagon path
                using (var path = new SKPath ()) {
                    path.MoveTo (71.4311121f, 56f);
                    path.CubicTo (68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
                    path.LineTo (43.0238921f, 97.5342563f);
                    path.CubicTo (41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
                    path.LineTo (64.5928855f, 143.034271f);
                    path.CubicTo (65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
                    path.LineTo (114.568946f, 147f);
                    path.CubicTo (117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
                    path.LineTo (142.976161f, 105.465744f);
                    path.CubicTo (144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
                    path.LineTo (121.407172f, 59.965729f);
                    path.CubicTo (120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
                    path.LineTo (71.4311121f, 56f);
                    path.Close ();

                    // draw the Xamagon path
                    canvas.DrawPath (path, paint);
                }
            }
        };

        form.Content = skControl;
        */
        application.Run(form);
       
    }
    
    
}