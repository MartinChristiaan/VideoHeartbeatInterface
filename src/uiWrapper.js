module.exports = {
    AddToParent:function(parent,child)
    {
        parent.appendChild(child);

    },
    SetSliderValue:function(slider,value)
    {
        slider.value = value     
    },
    SetSliderOnInput:function(slider,onchange)
    {
        slider.oninput = function() {
            onchange(this.value)
            }  
    },


    CreateSlider:function(min,max,step)
    {
        var slider =document.createElement("input")
        slider.type = "range"
        
        slider.min = min
        slider.className = "slider"
        slider.max = max
        slider.step = step
        slider.value = (min + max)/2
        return slider
    },
    CreateNumericInput:function(value,onchange)
    {
        var input =document.createElement("input")
        input.type = "number"
        input.className = "input is-rounded is-small"
        input.value = value
        input.oninput = function() {
            onchange(this.value)
            }
        return input
    },
    AdjustNumericInput:function(name,value,onchange)
    {
        var input =document.getElementById(name)
        input.value = value
        input.oninput = function() {
            onchange(this.value)
            }
        console.log(input.value)
        return input
    },
    SetType:function(type,element)
    {
        element.type = type
        return element
    }
    
}