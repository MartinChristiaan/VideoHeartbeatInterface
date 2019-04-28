module UIElements
open Fable.Import.Browser
open Layout
open Communication


let createSliderWidget (uiData : string array) =
    let widgetContainer = document.createElement("div")
    widgetContainer.setAttribute("class","widgetcontainer")
   
    let index = uiData.[0]
    let uiname = uiData.[1]
    let min = uiData.[2]|>float
    let max = uiData.[3]|>float
    let mutable value = uiData.[4]|>float

    let text = createDefaultUIHeader uiname
    let communicateInputData() = 
        UpdateField index (value.ToString()) 
    let onSliderValueChange newvalue =
       value<-newvalue
       text.innerText <- uiname + " : "+ value.ToString()
       communicateInputData()
    onSliderValueChange value
    let slider = uiutil.CreateSlider min max value onSliderValueChange
    createStandardUIContainer text slider    

let createSwitchWidget (uiData : string array) =
    let widgetContainer = document.createElement("div")
    widgetContainer.setAttribute("class","widgetcontainer")
    let index = uiData.[0]
    let uiname = uiData.[1]
    let mutable isChecked = uiData.[2] = "True"
    
    let text = createDefaultUIHeader uiname
   
    
    let switch = createElement "input"  "switch is-rounded"|>uiutil.SetType "checkbox"
    let communicateInputData () = 
  
        isChecked <-  not isChecked
        
        if isChecked then
            switch.setAttribute("Checked","checked")
        else
            switch.removeAttribute("Checked")
        
        UpdateField index (isChecked.ToString()) 
    if isChecked then
       switch.setAttribute("Checked","checked")
    
    
    let label = createElement "label" ""
    let subdiv = createElement "div" ""
    addChildren subdiv [switch;label] 
    subdiv.onclick <- (fun x -> communicateInputData())
    createStandardUIContainer text subdiv    

let createButtonWidget (uiData : string array) =
    
    let index = uiData.[0]
    let uiname = uiData.[1]
    let communicateInputData() = 
        UpdateField index ("_") 
    let button = createElement "button" "button"
    button.innerText<-uiname
    button.onclick <- (fun x -> communicateInputData())
    let subdiv = createElement "div" ""
    createStandardUIContainer subdiv button  

let orderHearbeatWidget = 
    let svgString = getGlobalRequest "https://vectr.com/mtin/a8oYzV9MHR.svg?width=640&height=640&select=a8oYzV9MHRpage0"
    document.getElementById("textid").children.[0].textContent<- "30"

let createHeartbeatWidget = 

