module Util

let last n xs = 
    let len = Array.length xs
    if len > n then
        Array.skip ((Array.length xs) - n) xs
    else
        xs
let range min max xs = 
    let len = Array.length xs
    let delta= max - min
    Array.skip ((Array.length xs) - min) xs|>Array.take delta


let (<*>) fs xs = Array.map2 (fun f x -> f x) fs xs
let map3 f xs bs cs = Array.map2 f xs bs <*> cs
let map4 f xs bs cs ds = map3 f xs bs cs <*> ds
let map5 f xs bs cs ds es = map4 f xs bs cs ds <*>  es

type Time = float
type TimedDataCallback = string array -> Time -> unit
