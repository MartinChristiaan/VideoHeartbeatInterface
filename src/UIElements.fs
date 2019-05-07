module UIElements
open Fable.Import.Browser
open Layout
open Communication


let createSliderWidget (classname:ClassName) (fieldname:FieldName) name min max step =
    
    let widgetContainer = document.createElement("div")
    widgetContainer.setAttribute("class","widgetcontainer")
    let text = createDefaultUIHeader name
    
    // let onSliderValueChange newvalue =
    //    text.innerText <- name + " : "+ newvalue.ToString()

    let slider = uiutil.CreateSlider min max step 
    slider.className <- slider.className + " SliderData " + classname + " " + fieldname 
    createStandardUIContainer text slider 

let connectSlider (dataListener:Element*ClassName*FieldName) (data : string) =
    let slider,classname,fieldname = dataListener
    let text = slider.parentElement.children.[0]:?>HTMLElement
    let name = text.innerText

    let updateSlider value =
        (http.updateTarget (BaseURL+updateTargetURL) classname fieldname "float" (string value))
        text.innerText <- name + " : "+ value.ToString()
    
    uiutil.SetSliderValue slider (float data)
    text.innerText <- name + " : "+ data
    uiutil.SetSliderOnInput slider updateSlider
   
let connectSwitch (dataListener:Element*ClassName*FieldName) (data : string) = 
    let switch,classname,fieldname = dataListener
    let mutable isChecked = data = "True"
    if isChecked then
        switch.setAttribute("Checked","checked")
    else
        switch.removeAttribute("Checked")
    let communicateInputData () = 
        isChecked <-  not isChecked
        if isChecked then
            switch.setAttribute("Checked","checked")
        else
            switch.removeAttribute("Checked")
        http.updateTarget (BaseURL+updateTargetURL) classname fieldname "bool" (string isChecked)
    switch.parentElement.onclick <-(fun x -> communicateInputData())

let createSwitchWidget label classname fieldname =
    let widgetContainer = document.createElement("div")
    widgetContainer.setAttribute("class","widgetcontainer")
    let uiname = label
    let text = createDefaultUIHeader uiname
    let switch = createElement "input"  ("switch is-rounded " + "SwitchData" + " " + classname + " " + fieldname)|>uiutil.SetType "checkbox"
  
    let label = createElement "label" ""
    let subdiv = createElement "div" ""
    addChildren subdiv [switch;label] 
    createStandardUIContainer text subdiv    

let createButtonWidget label classname method =
    
    let uiname = label
    let button = createElement "button" "button"
    button.innerText<-uiname
    button.onclick <- (fun x -> http.invokeMethod (BaseURL+invokeMethodURL) classname method)
    let subdiv = createElement "div" ""
    createStandardUIContainer subdiv button  

// let orderHearbeatWidget = 
//     let svgString = getGlobalRequest "https://vectr.com/mtin/a8oYzV9MHR.svg?width=640&height=640&select=a8oYzV9MHRpage0"
//     document.getElementById("textid").children.[0].textContent<- "30

// let createHeartbeatWidget = 

