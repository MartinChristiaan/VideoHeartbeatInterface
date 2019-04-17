module App


open Layout
open Chart
open Fable.Import.Browser

open Communication



let startstopbtn = document.getElementById("StartStop")

let toggleStop() = getRequest ("toggleStop") 

let restartClick restart x =
    toggleStop()|>ignore
    startstopbtn.innerText <- "Pause"
    startstopbtn.onclick <- restart
    startstopbtn.className <- "button is-danger"

let rec pauzeClick(x) =
    toggleStop()|>ignore
    startstopbtn.innerText <- "Restart"
    let restartFun = restartClick pauzeClick 
    startstopbtn.onclick <- restartFun
    startstopbtn.className <- "button is-primary"
 
startstopbtn.onclick <- pauzeClick


hello|>console.log