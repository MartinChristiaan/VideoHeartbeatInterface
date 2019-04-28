module Dropdowns
open Layout
open Communication 
open Fable.Import.Browser


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


let createDropDownMenu (instructions : string array)  =
    
    let index = instructions.[0]
    let uiname = instructions.[1]
    let defaultValue = instructions.[2]
    let options = instructions|>Array.skip 3

    let onSelected value = 
        UpdateField index value

    let dropDownMenu = document.createElement("div")
    dropDownMenu.className <- "dropdown"
    
    let ddbtn = fancyDropDownBtn defaultValue|> createElementFromHTML:?>HTMLElement
    
    let hideDropDown reset x = 
        dropDownMenu.className <- "dropdown"
        dropDownMenu.onclick <-reset

    let rec unhideDropDown x =
        dropDownMenu.className <- "dropdown is-active"
        let hideDropDownFun = hideDropDown unhideDropDown
        dropDownMenu.onclick <-hideDropDownFun
    dropDownMenu.onclick<-unhideDropDown    
    
    let menu = createElement "div" "dropdown-menu"
    menu.setAttribute("role","menu")
    let content = createElement "div" "dropdown-content"
    let btn = ddbtn.children.Item 0:?>HTMLElement
    let label = btn.children.Item 0 :?> HTMLElement
    

    let deactivateDropDown (dropDownElement:HTMLElement) =
        dropDownElement.className<-"dropdown-item" 

    let dropDownElements = options|>Seq.map createDropDownOption|>Seq.toList 
    let dropDownOnSelect (dropDownElement : HTMLElement) x = 
        onSelected dropDownElement.innerText
        dropDownElements|>Seq.iter(fun x -> x.className <- "dropdown-item")
        dropDownElement.className<-"dropdown-item is-active"
        label.innerText <- dropDownElement.innerText
        

    let setOnSelect (dropDownElement : HTMLElement) = 
        let myDropDownFun = dropDownOnSelect dropDownElement
        dropDownElement.onclick <- myDropDownFun

    for i in [0..dropDownElements.Length-1] do
        dropDownElements.[i]|>setOnSelect

    let defaultIdx = options|>Array.findIndex(fun x->x = defaultValue)
    dropDownOnSelect dropDownElements.[defaultIdx] defaultIdx
   
    addChildren content dropDownElements
    addChildren menu [content]
    addChildren dropDownMenu [ddbtn;menu]
    let header = createDefaultUIHeader uiname
    createStandardUIContainer header dropDownMenu



