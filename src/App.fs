module App


open Layout
open Chart
open Fable.Import.Browser
open Receiver
open Communication
open UIElements
open Dropdowns
open PythonTypes
open Fable.Core.JsInterop
open Fable.Import.Browser
open Fable.Import
open Fable
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser

open Browser
open Fable.Core.JsInterop


open System
open Util
// createslider
//let createHueSlider : DataCallback = 


let createClass name (elements:((ClassName*FieldName)*DataCallback) list) (otherElements:HTMLElement list) = 
    let classParent,classContainer = createClassContainer name
    getTargets elements (addChildren classContainer)
    otherElements|>List.iter (uiutil.AddToParent classContainer) 
    classParent|> addToControl 
let setupfigures (input) =
    let dataAdresses =  input|>List.map(fun ((updateMethods,elements),dataAdresses) -> dataAdresses)
    let callbacks =  input|>List.map(fun ((updateMethods,elements),dataAdresses) -> updateMethods)|>List.toArray
    getFigureTargets dataAdresses callbacks DateTime.Now 0.0
    input|>List.map(fun ((_,elements),_) -> elements)|>List.iter addToVisual 
    

//createClass "Main" [Main_display,createDropDownMenu "Display" Main_display_options] 
  //                  [createButtonWidget "Reset Measurements" Main_resetMeasurement]

type AudioWrapper = 
    abstract start_audio_player : float array -> float array -> (unit -> float)
open Fable.Core.JsInterop

[<Import("*", "./audiowrapper")>]
let audioWrapper : AudioWrapper = jsNative

let oneset_times = [|5.0;5.0;5.0;10.0;|]
let onset_frequenties = [|1.0;2.0;3.0;1.0|]
let getDuration = audioWrapper.start_audio_player oneset_times onset_frequenties



let func() = 
    updateTarget Main_percent (getDuration()|>string) "float" 
    let classname,methodname = Main_update_chroma
    http.invokeMethod (BaseURL+invokeMethodURL) classname methodname 
//http.performAfter func 5000.0




//setupfigures [createReplacingFigure [|"Normalized Amplitude (chrom)"|] "Frequency",[Main_idx;Main_chroma]
//              createTimeFigure [|"Onsets"|],[Main_curbeat]]


// How to do the animation?

// Capture inputs?





