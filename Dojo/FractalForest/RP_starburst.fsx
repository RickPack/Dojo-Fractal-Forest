﻿open System
open System.Drawing
open System.Windows.Forms

// Create a form to display the graphics
let width, height = 1000, 850
let form = new Form(Width = width, Height = height)
let box = new PictureBox(BackColor = Color.White, Dock = DockStyle.Fill)
let image = new Bitmap(width, height)
let graphics = Graphics.FromImage(image)
//The following line produces higher quality images, 
//at the expense of speed. Uncomment it if you want
//more beautiful images, even if it's slower.
//Thanks to https://twitter.com/AlexKozhemiakin for the tip!
//graphics.SmoothingMode <- System.Drawing.Drawing2D.SmoothingMode.HighQuality
let brush = new SolidBrush(Color.FromArgb(0, 0, 0))

box.Image <- image
form.Controls.Add(box) 

// Compute the endpoint of a line
// starting at x, y, going at a certain angle
// for a certain length. 
let endpoint x y angle length =
    x + length * cos angle,
    y + length * sin angle

let flip x = (float)height - x

// Utility function: draw a line of given width, 
// starting from x, y
// going at a certain angle, for a certain length.
let drawLine (target : Graphics) (brush : Brush) 
             (x : float) (y : float) 
             (angle : float) (length : float) (width : float) =
    let x_end, y_end = endpoint x y angle length
    let origin = new PointF((single)x, (single)(y |> flip))
    let destination = new PointF((single)x_end, (single)(y_end |> flip))
    let pen = new Pen(brush, (single)width)
    target.DrawLine(pen, origin, destination)

let draw x y angle length width = 
    drawLine graphics brush x y angle length width

let pi = Math.PI

// Now... your turn to draw
// The trunk
// draw 250. 50. (pi*(0.5)) 100. 4.

let rec anglefac x y angle length width =     
    let newangle = angle + 30.0
    let newx = x + 1.0
    let newy = y + 1.0
    if newangle <1440.0 && newx <1000.0 && newy<850.0 then         
        draw newx newy newangle length width
        let newx2 = x + 5.0
        let newy2 = y + 5.0
        draw newx2 newy2 newangle length width        
        anglefac newx newy newangle length width

anglefac 250. 200. 30.0 100. 4.

form.ShowDialog()

// Code from the original script to draw two branches 
//
//let rec counter = 
//let x, y = endpoint 250. 50. (pi*(0.5)) 100.
//// first and second branches
//draw x y (pi*(0.5 + 0.3)) 50. 2.
//draw x y (pi*(0.5 - 0.4)) 50. 2.
//let x = float(x)
//draw x y (pi*(0.5 + 0.3)) 50. 2.
//draw x y (pi*(0.5 - 0.4)) 50. 2.
//form.ShowDialog()

(* To do a nice fractal tree, using recursion is
probably a good idea. The following link might
come in handy if you have never used recursion in F#:
http://en.wikibooks.org/wiki/F_Sharp_Programming/Recursion
*)
