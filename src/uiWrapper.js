module.exports = {
    AddToParent:function(parent,child)
    {
        parent.appendChild(child);

    },

    CreateSlider:function(min,max,value,onchange)
    {
        var slider =document.createElement("input")
        slider.type = "range"
        
        slider.min = min
        slider.className = "slider"
        slider.max = max
        slider.step = (max-min)/100
        slider.value = value
        slider.oninput = function() {
            onchange(this.value)
            }
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
    }
    
}