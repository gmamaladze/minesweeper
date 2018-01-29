module Geometry

type Point = 
    { X : int
      Y : int }

type Scale = Point

type Direction = 
    { DX : int
      DY : int }
    
    static member (+) (first : Direction, second : Direction) = 
        { DX = first.DX + second.DX
          DY = first.DY + second.DY }
    
    static member (+) (first : Point, second : Direction) = 
        { X = first.X + second.DX
          Y = first.Y + second.DY }
    
    static member Up = 
        { DX = 0
          DY = (-1) }
    
    static member Down = 
        { DX = 0
          DY = 1 }
    
    static member Left = 
        { DX = (-1)
          DY = 0 }
    
    static member Right = 
        { DX = 1
          DY = 0 }
    
    static member All = 
        [ Direction.Up
          Direction.Up + Direction.Right
          Direction.Right
          Direction.Right + Direction.Down
          Direction.Down
          Direction.Down + Direction.Left
          Direction.Left
          Direction.Left + Direction.Up ]

type Size = 
    { Width : int
      Height : int }
    
    static member (+) (left : Size, right : Point) : Size = 
        { Width = left.Width + right.X
          Height = left.Height + right.Y }
    
    static member (*) (left : Size, right : Scale) = 
        { Width = left.Width * right.X
          Height = left.Height * right.Y }
    
    member x.FitsInto(other : Size) = (x.Width <= other.Width) && (x.Height <= other.Height)
    member x.AllPoints = 
        seq { 
            for i in 0..x.Width do
                for j in 0..x.Height do
                    yield { X = i
                            Y = j }
        }

type Point with
    member x.IsInRange(size : Size) = (x.X) >= 0 && (x.Y) >= 0 && (x.X < size.Width) && (x.Y < size.Height)
    member x.Next(direction : Direction) = x + direction
    member x.Neighbours() = Direction.All |> Seq.map (fun d -> x + d)
    member x.Neighbours(range : Size) = x.Neighbours() |> Seq.filter (fun p -> p.IsInRange(range))
    member x.Move(direction : Direction, size : Size) = 
        let next = x.Next(direction)
        match next.IsInRange(size) with
        | true -> next
        | _ -> x
