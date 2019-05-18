module Dropdowns
open Layout
open Communication 
open Fable.Import.Browser

let weirdListToList (weirdList:NodeList) =
    let mutable normalList = []
    for i in [0..(int weirdList.length)-1] do
        normalList <- normalList @ [weirdList.[i]:?>HTMLElement]
    normalList


let createDropDownOption (name : string)   =
    let dropDownElement = createElement "a" "dropdown-item"
    dropDownElement.innerText <-name
    dropDownElement.onclick<- fun x -> console.log "test"
 
    dropDownElement

let fancyDropDownBtn name = "<div class=\"dropdown-trigger\">
                    <button class=\"button\" aria-haspopup=\"true\" aria-controls=\"dropdown-menu\">
                      <span id=\"ddbtnlabel\">"+name+"</span>
                      <span class=\"icon is-small\">
                        <i class=\"fas fa-angle-down\" aria-hidden=\"true\"></i>
                      </span>
                    </button>
                  </div>"


let createDropDownMenu labeltxt (options:string list) (classname,fieldname) (data:string) =
       
    
    let dropdown = document.createElement("div")
    dropdown.className <- "dropdown"

    let hideDropDown reset x = 
         dropdown.className <- "dropdown" 
         dropdown.onclick <-reset

    let rec unhideDropDown x =
        dropdown.className <- "dropdown is-active" 
        let hideDropDownFun = hideDropDown unhideDropDown
        dropdown.onclick <-hideDropDownFun
    dropdown.onclick<-unhideDropDown    

    let ddbtn = fancyDropDownBtn options.[0]|> createElementFromHTML:?>HTMLElement
    
    let dropDownElements = options|>Seq.map createDropDownOption|>Seq.toList 
    let content = createElement "div" "dropdown-content"
   
    addChildren content dropDownElements
    let menu = createElement "div" "dropdown-menu"
    menu.setAttribute("role","menu")
   
                                   // menu         content        
 
    
    let label = (ddbtn.children.[0]:?>HTMLElement).children.[0]:?>HTMLElement 
    
  
    let dropDownOnSelect (dropDownElement : HTMLElement) x = 
        http.updateTarget (BaseURL+updateTargetURL) classname fieldname "enum" dropDownElement.innerText 
        dropDownElements|>Seq.iter(fun x -> x.className <- "dropdown-item")
        dropDownElement.className<-"dropdown-item is-active"
        label.innerText <- dropDownElement.innerText
        
    let setOnSelect (dropDownElement : HTMLElement) = 
         let myDropDownFun = dropDownOnSelect dropDownElement
         dropDownElement.onclick <- myDropDownFun

    for i in [0..dropDownElements.Length-1] do    
        dropDownElements.[i]|>setOnSelect
    let deactivateDropDown (dropDownElement:HTMLElement) =
         dropDownElement.className<-"dropdown-item" 

    let options = dropDownElements|>List.map(fun x -> x.innerText)

    console.log data
    let defaultIdx = options|>List.findIndex(fun x -> x = (data.Split('.').[1] ))
    dropDownOnSelect dropDownElements.[defaultIdx] defaultIdx
 
   
    addChildren menu [content]
    addChildren dropdown [ddbtn;menu]
    let header = createDefaultUIHeader labeltxt
    createStandardUIContainer header dropdown



