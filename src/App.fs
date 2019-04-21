module App


open Layout
open Chart
open Fable.Import.Browser
open Receiver
open Communication
open Sliders
open Dropdowns

let createElementFromInstructions (classMap : Map<string,HTMLElement> )  (instructions : string array)= 
    
    let index = instructions.[0]
    let uitype = instructions.[1]
    let className = instructions.[2]
    let uiinstructions = Array.append [|index|] (instructions|>Array.skip 3)

 
    let element = 
        match uitype with 
        |_ when uitype = "slider" -> createSliderWidget uiinstructions 
        |_ when uitype = "dropdown" -> createDropDownMenu uiinstructions
        |_ when uitype = "switch" -> createSwitchWidget uiinstructions 
    let foundClass = classMap.TryFind className
    let classContainer,newclassMap = 
        match foundClass with
        | Some classContainer -> classContainer,classMap
        | None -> 
                let container = createElement "div" "categoryContainer"
                container,classMap.Add (className,container)

    uiutil.AddToParent classContainer element
    newclassMap

    //let elements = category.uiElementInstructions|>List.map(createElementFromInstructions category.name)
//    addChildren categorysubContainer elements

let createClassContainer (categoryname : string,categorysubContainer) =
    let categoryContainer = createElement "div" "categoryMainContainer card  has-text-centered"
    let categoryLabel = createElement("p") "card is-capitalized"
    categoryLabel.innerText <- categoryname
    addChildren categoryContainer [categoryLabel; categorysubContainer]
    categoryContainer



let rec updateFigures figures (newdata : string array list) =
    let newfigures = List.map2 updateFigure figures newdata
    let curriedUpdateFigures = updateFigures newfigures
    timedGetRequest (Receiver.getPlotDataFromJson>>curriedUpdateFigures)


let handleUIInstructions data = 
    let elementInstructions = Receiver.getInputDataFromJson data
    let figureInstructions,controlInstructions = elementInstructions|>List.partition(fun x -> x.[1] = "figure" ) // split between figures and control elements
    console.log "figureInstructions"
    console.log figureInstructions
    // Creates all elements, Assigns them to classes and adds the classes to the controldiv
    List.fold createElementFromInstructions Map.empty controlInstructions
    |>Map.toList|>List.map createClassContainer|> addChildren controldiv    

    let createdfigures = List.map createFigure figureInstructions
    createdfigures|>List.map(fun (x,canvas,y,z) -> canvas)|> addChildren visualdiv  // Adds vizualdivs 
    let figures = createdfigures|>List.map(fun (x,canvas,y,z) -> x,y,z)
    let curriedUpdateFigures = updateFigures figures
    timedGetRequest (Receiver.getPlotDataFromJson>>curriedUpdateFigures)


    // Next ew should setup update functions
    // infinite whil

 // Execution will pause until fetchHtmlAsync finishes



getRequest getInstructionsURL handleUIInstructions|>ignore





// let onStatesReceived states =
//     let figure = createFigure states
//     let updateplotfun values= updatePlot values 0.01 figure 
//     Communication.getValues 100.0 updateplotfun

// Communication.getInputs createSliderWidgets|>ignore


// let startstopbtn = document.getElementById("StartStop")


// let restartClick restart x =
//     Communication.timer.Stop()
//     startstopbtn.innerText <- "Start"
//     startstopbtn.onclick <- restart
//     startstopbtn.className <- "button is-primary"

// let rec startClick(x) =
//     Communication.timer.Start()
//     startstopbtn.innerText <- "Pauze"
//     let stopTimerFun = restartClick startClick 
//     startstopbtn.onclick <- stopTimerFun
//     startstopbtn.className <- "button is-danger"

// Communication.getStates onStatesReceived|>ignore
// startstopbtn.onclick <- startClick




//Layout.uiutil.AdjustNumericInput "interval" 100.0 (fun v -> Communication.timer.Interval <- v) |>ignore

    
//let dropdown = document.getElementById("mainDropDown")



// let ddm = createDropDownMenu ["option1";"option2";"option3"] console.log
// ddm|>addToControl
