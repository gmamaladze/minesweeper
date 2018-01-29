module Adapter

open Board
open Fmines.DotNet
open Gameplay
open Geometry
open System.Collections.Generic
open System.Collections.Immutable

let public c (size : Fmines.Geometry.Size) = 
    { Width = size.Width
      Height = size.Height }

let public p (point : Point) = new Fmines.Geometry.Point(point.X, point.Y)
let private s (size : Size) = new Fmines.Geometry.Size(size.Width, size.Height)

let public Draw(graphics : Fmines.Ui.Graphics, mf : MineField, gs : GameState) = 
    let mines = 
        (mf.Mines
         |> Set.toSeq
         |> Seq.map (fun x -> p (x))).ToReadOnlySet()
    
    let warnings = 
        mf.Warnings
        |> Map.toSeq
        |> Seq.map (fun (k, v) -> (p (k), v))
        |> dict
    
    let cmf = new Fmines.MineField(s (mf.Size), mines, warnings)
    
    let covers = 
        (gs.Covers.Covers
         |> Map.toSeq
         |> Seq.map (fun (k, v) -> (p (k), v))
         |> dict).ToImmutableDictionary()
    
    let ccv = new Fmines.Covers(covers)
    Fmines.Ui.Board.Draw(graphics, cmf, ccv)
    Fmines.Ui.Cursor.Draw(graphics, p (gs.CursorPosition))
