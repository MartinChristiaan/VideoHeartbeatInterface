module App


open Layout
open Chart
open Fable.Import.Browser
open Receiver
open Communication
open UIElements
open Dropdowns
open PythonTypes



// createslider
getTargets [SkinClassifier_minh;SkinClassifier_maxh] [|console.log;console.log|]



// type DataRetreiver = string -> unit
// type DataRequest = HTMLElement * DataRetreiver
// type ConnectionMethod = (Element*ClassName*FieldName) -> string -> unit
// let someswitch = createSwitchWidget "Enabled" "SkinClassifier" "enabled"
// let someDropDown = createDropDownMenu "Display" ["Source";"Post FaceDetect";"Post SkinClassifier"] "Main" "display"  

// let slider = createSliderWidget "SkinClassifier" "minh" "Min Hue" 0.0 255.0 255.0

// let someButton = createButtonWidget "DoSomething" "Main" "resetMeasurement"

// addChildren controldiv [someDropDown;someswitch;slider;someButton]

// let fpschart = createTimeFigure [|"FPS"|] "Main" "fps"
// addChildren visualdiv [fpschart]

// let getDataListeners dataClassname = 
//     let sliderDataElements = document.getElementsByClassName(dataClassname)
//     let mutable dataListeners : (Element*ClassName*FieldName) list = []
    
//     for i in [0..(int sliderDataElements.length-1)] do
//         let classes  = sliderDataElements.[i].classList
//         for j in [0..(int classes.length-1)] do
//             if classes.[j] = dataClassname then
//                 dataListeners <- dataListeners @ [(sliderDataElements.[i],classes.[j+1],classes.[j+2])]
//     let classnames = dataListeners|>List.map(fun (el,cn,fldn) -> cn)|>String.concat " "  // create dataRequest
//     console.log dataClassname
//     let fieldnames = dataListeners|>List.map(fun (el,cn,fldn) -> fldn)|>String.concat " "  // create dataRequest
//     dataListeners,classnames,fieldnames

// let updateUIElement dataListeners (connectionMethod : ConnectionMethod) (data : string) =
//     let items= data.Split ','|> splitInto 2
//     Seq.iter2 connectionMethod dataListeners items.[1]

// let requestConnection  (connectionMethod:ConnectionMethod) (dataListeners,classnames,fieldnames)= 
//     let url = (BaseURL+GetTargetsURL)
//     console.log url
//     http.getTargets url classnames fieldnames (updateUIElement  dataListeners connectionMethod) 0
//     ()
// let DataClasses = ["SliderData";"SwitchData";"DropDownData"]
// let connectionMethods : ConnectionMethod list= [connectSlider;connectSwitch;connectDropDown]
// List.map getDataListeners DataClasses|>List.iter2 requestConnection connectionMethods  

