module Receiver

open Fable.Import
open Fable.Import.Browser
open System
open Browser
open Communication

type Category = 
  {
   name : string;
   uiElementInstructions : string array list;   
  }

let dataRequest classname fieldname callback= 


let removeChar (unwanted : string) (input:string)=
    input.Replace(unwanted,"")

let everyNth n offset seq = 
  seq |> Array.mapi (fun i el -> el, i)              // Add index to element
      |> Array.filter (fun (el, i) -> i % n = offset) // Take every nth element
      |> Array.map fst   

let getInputDataFromJson (text:string) =

    let items = text.Split ','
    let rec getElements (items: string array) (elementDatas:string array list) = 
        let numberOfElements = items|>Array.head|>int
        if numberOfElements = 0 then
  
            elementDatas
        else
            let tailData = items|>Array.tail
            let elementData,remaining = tailData|>Array.splitAt numberOfElements
            let newElementDatas = elementDatas @ [elementData]
            if remaining|>Array.length > 0 then  
               getElements remaining newElementDatas
            else
               newElementDatas

    let elementInstructions = getElements items []
    elementInstructions
    
let getPlotDataFromJson (text:string) =
    let items = text.Split ','
    let rec getElements (items: string array) (plotValues:string array list) = 
        
  
        let xlen = items|>Array.head|>int
        let noDatasets = items.[1]|>int
        
        let elementData,remaining = items|>Array.splitAt (2 + xlen * (noDatasets+1))
        let newElementDatas = plotValues @ [elementData]
        if remaining|>Array.length > 0 then  
           getElements remaining newElementDatas
        else
           newElementDatas

    let elementInstructions = getElements items []
    elementInstructions
   


    
    
