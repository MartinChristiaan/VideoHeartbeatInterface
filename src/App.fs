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
    
console.log Main_display_options
createClass "Main" [Main_display,createDropDownMenu "Display" Main_display_options] 
                    [createButtonWidget "Reset Measurements" Main_resetMeasurement]

createClass "SkinClassifier" [SkinClassifier_minh,createSliderWidget "Min Hue" 0.0 255.0 1.0;
                              SkinClassifier_maxh,createSliderWidget "Max Hue" 0.0 255.0 1.0;
                              SkinClassifier_mins,createSliderWidget "Min Saturation" 0.0 255.0 1.0;
                              SkinClassifier_maxs,createSliderWidget "Max Saturation" 0.0 255.0 1.0;
                              SkinClassifier_minv,createSliderWidget "Min Value" 0.0 255.0 1.0;
                              SkinClassifier_maxv,createSliderWidget "Max Value" 0.0 255.0 1.0;
                              SkinClassifier_blursize, createSliderWidget "Blur size" 0.0 40.0 1.0;
                              SkinClassifier_elipse_size, createSliderWidget "Elipse Size" 0.0 20.0 1.0;
                              SkinClassifier_dilations, createSliderWidget "Dilations" 0.0 10.0 1.0;
                              SkinClassifier_erosions, createSliderWidget "Erotions" 0.0 10.0 1.0;
                              SkinClassifier_classifierType,createDropDownMenu "Classifier Type" SkinClassifier_classifierType_options;
                             
                              SkinClassifier_enabled,createSwitchWidget "Enabled";]
                               [] 

createClass "Face Detection" [FaceTracker_enabled, createSwitchWidget "Enabled"]
                            [createButtonWidget "Reset Tracker" FaceTracker_resetTracker]

createClass "Pulse Detection" [PulseDetector_useChrom,createSwitchWidget "Use Chrominance Method";
                                PulseDetector_usePBV, createSwitchWidget "Use PBV Method"]
                                []
createClass "Recorder" [Recorder_enabled, createSwitchWidget "Enabled"]
                        [createButtonWidget "Save Recording" Recorder_save_recording]                                

setupfigures [createTimeFigure [|"FPS"|],[Main_fps];
              createTimeFigure [|"Beats Per Minute (chrom)";"Beats Per Minute (PBV)"|],[PulseDetector_chromBPM;PulseDetector_PBVBPM];
              createTimeFigure [|"Signal to Noise Ratio (chrom)";"Signal to Nouse Ratio (PBV)"|],[PulseDetector_chromSNR;PulseDetector_PBVSNR];
              createTimeFigure [|"Num skin pixels"|],[SkinClassifier_num_skin_pixels];
              createReplacingFigure [|"Normalized Amplitude (chrom)"; "Normalized Amplitde (PBV)"|] "Frequency (BPM)",[PulseDetector_f;PulseDetector_normalized_amplitude_chrom;PulseDetector_normalized_amplitude_PBV]]



