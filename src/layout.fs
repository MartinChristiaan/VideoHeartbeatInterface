module Layout
open Fable.Import
open Fable
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Browser

open Browser
open Fable.Core.JsInterop
open Communication
let controldiv = document.getElementById("control")


type layoutWrapper = 
    abstract AddToParent : HTMLElement -> HTMLElement -> unit
    abstract CreateSlider  : float -> float -> float -> (float -> unit) -> HTMLElement
    abstract CreateNumericInput  : float -> (float -> unit) -> HTMLElement
    abstract AdjustNumericInput  : string -> float -> (float -> unit) -> HTMLElement
    
// type SliderParam = 
//   {
//     name : string;
//     min : float;
//     max : float;
//     value : float;    
//   }

[<Import("*", "./uiWrapper")>]
let uiutil : layoutWrapper = jsNative

 
let addToControl element = uiutil.AddToParent controldiv element
let createSliderWidget (inputData:InputData) categoryDiv categoryName =
    let widgetContainer = document.createElement("div")
    widgetContainer.setAttribute("class","widgetcontainer")
    let sliderTextContainer = document.createElement("div")
    let minTextContainer = document.createElement("div")
    let maxTextContainer = document.createElement("div")
   
    sliderTextContainer.setAttribute("class","column has-text-centered")
    minTextContainer.setAttribute("class","column has-text-centered")
    maxTextContainer.setAttribute("class","column has-text-centered")
   
    let text = document.createElement("p")
    text.className <- "is-size-7"
    let mutable value = inputData.value
    let mutable min = inputData.min
    let mutable max = inputData.max
    let communicateInputData() = 
        setInputData inputData.name categoryName value min max 
    


    let onSliderValueChange newvalue =
       value<-newvalue
       text.innerText <- inputData.name + " : "+ value.ToString()
       communicateInputData()
    onSliderValueChange inputData.value
    
     
    let slider = uiutil.CreateSlider min max value onSliderValueChange

    let updateSliderMin newMin = 
        min<-newMin
        slider.setAttribute("min",min.ToString())
        communicateInputData()

    let updateSliderMax newMax = 
        max<-newMax
        slider.setAttribute("max",max.ToString())
        communicateInputData()

    let minInput = uiutil.CreateNumericInput inputData.min updateSliderMin
    let maxInput = uiutil.CreateNumericInput inputData.max updateSliderMax
    

    let mintext = document.createElement("p")
    mintext.innerText <- "min"
    mintext|> uiutil.AddToParent minTextContainer
    minInput|> uiutil.AddToParent minTextContainer
  

 //   minTextContainer|> uiutil.AddToParent widgetContainer

    text|>uiutil.AddToParent sliderTextContainer        
    slider|> uiutil.AddToParent sliderTextContainer 
    sliderTextContainer|>uiutil.AddToParent widgetContainer

    let maxtext = document.createElement("p")
    maxtext.innerText <- "max"
    maxtext|> uiutil.AddToParent maxTextContainer
    maxInput|> uiutil.AddToParent maxTextContainer
    //maxTextContainer|> uiutil.AddToParent widgetContainer

    widgetContainer|>uiutil.AddToParent categoryDiv



let createSliderWidgets (nameValuePairs:InputData array,categories:Category seq) =
    let mutable index = 0
    for cat in categories do
        let categoryContainer = document.createElement("div")
        categoryContainer.setAttribute("class","categoryMainContainer card  has-text-centered")
        let categoryLabel = document.createElement("p")
        categoryLabel.innerText <- cat.name
        categoryLabel.className <-"card is-capitalized"
        categoryLabel|> uiutil.AddToParent categoryContainer
        let categorysubContainer = document.createElement("div")
        categorysubContainer.setAttribute("class","categoryContainer")
        categorysubContainer|>uiutil.AddToParent categoryContainer
                
        nameValuePairs.[index..(index + cat.noItems-1)]|>Array.iter (fun d -> createSliderWidget d categorysubContainer cat.name)   
        index <- index + cat.noItems        
        categoryContainer|>addToControl
    





// let param = {name = "TestParameter"; min = 0.0; max = 100.0; value = 60.0 }
// createSliderWidget param



