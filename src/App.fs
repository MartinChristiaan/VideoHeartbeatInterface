module App


open Layout
open Chart
open Fable.Import.Browser
open Receiver
open Communication
open UIElements
open Dropdowns
open PythonTypes


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
    

createClass "Main" [Main_display,createDropDownMenu "Display" ["Source";"Post Face";"Post skin"]] 
                    [createButtonWidget "Reset Measurements" Main_resetMeasurement]

createClass "SkinClassifier" [SkinClassifier_minh,createSliderWidget "Min Hue" 0.0 255.0 1.0;
                              SkinClassifier_maxh,createSliderWidget "Max Hue" 0.0 255.0 1.0;
                              SkinClassifier_mins,createSliderWidget "Min Saturation" 0.0 255.0 1.0;
                              SkinClassifier_maxs,createSliderWidget "Max Saturation" 0.0 255.0 1.0;
                              SkinClassifier_minv,createSliderWidget "Min Value" 0.0 255.0 1.0;
                              SkinClassifier_maxv,createSliderWidget "Max Value" 0.0 255.0 1.0;
                              SkinClassifier_enabled,createSwitchWidget "Enabled";]
                               [] 
                               


setupfigures [createTimeFigure [|"FPS"|],[Main_fps];
              createTimeFigure [|"Max Hue"|],[SkinClassifier_maxh]
              createReplacingFigure [|"Normalized Amplitude"|] "Frequency (BPM)",[Evaluator_f;Evaluator_normalized_amplitude]]



