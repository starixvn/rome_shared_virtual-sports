if (typeof (Fanex) == 'undefined') { // Require Fanex namespace
    Fanex = {};
}
if (typeof (Fanex.PTVirtualSports) == 'undefined') { // Require Fanex.PTVirtualSports namespace
    alert('Require Fanex.PTVirtualSports namespace');
}
// Function name format Render_[RoleId_]TargetRoleId_Form
// [RoleId_] is optional
// Call method by Method.call(this, arg1, arg2...)

// Super edit Master
Fanex.PTVirtualSports.BetType.prototype.Render_0_3_1 = function (owner) { // s3, m3, us3, is3, n3
    box = document.createElement("DIV");

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BettingLimitHeader";
    box.appendChild(header); // Header bet type name

    if (typeof (this.pt.m3) == 'undefined') { // Add new form doesn't have current value, need to set defaut current value
        this.pt.n3 = 0;
        this.pt.m3 = 0;
        this.pt.s3 = this.pt.s4;
    }

    var m4 = this.pt.m4;
    var s4 = this.pt.s4;
    var n4 = this.pt.n4;
    var is3 = this.pt.is3;
    var s3 = this.pt.s3;
    var us3 = is3 ? this.pt.us3 : 0;

    // s3, us3 - free from 0 to s4 and control others
    var min = Math.max(0, Fanex.Round(n4 - m4));
    var max = s4;
    var selected = this.pt.is3 ? this.pt.us3 : this.pt.s3;

    var s3Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    s3Select.select.id = s3Select.select.name = "s3$" + this.suffixId;
    s3Select.className = "BetTypeSelect";
    s3Select.style.minWidth = "204px";
    box.appendChild(s3Select);

    // is3
    var is3Check = document.createElement("INPUT");
    is3Check.type = "checkbox";
    is3Check.checked = is3Check.defaultChecked = this.pt.is3;
    is3Check.name = is3Check.id = "is3$" + this.suffixId;
    var is3Div = document.createElement("DIV");
    is3Div.className = "BetTypeCheckbox";
    is3Div.style.minWidth = "85px";
    is3Div.appendChild(is3Check);
    box.appendChild(is3Div);

    // m3
    min = Math.max(0, Fanex.Round(n4 - (is3 ? us3 : s3)));
    var max = is3 ? m4 : Math.min(m4, Fanex.Round(this.pt.s4 - this.pt.s3));
    var selected = this.pt.m3;
    var m3Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    m3Select.select.id = s3Select.select.name = "m3$" + this.suffixId;
    m3Select.className = "BetTypeSelect";
    m3Select.style.minWidth = "204px";
    box.appendChild(m3Select);

    // n3
    var min = 0;
    var max = this.pt.m3;
    var selected = this.pt.n3;
    var n3Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    n3Select.select.id = s3Select.select.name = "n3$" + this.suffixId;
    n3Select.className = "BetTypeSelect";
    n3Select.style.minWidth = "204px";
    box.appendChild(n3Select);

    var pt = this.pt;
    // Event handling
    s3Select.select.onchange = function (e, keepCurrent) {
        // Update s3, us3
        if (is3Check.checked) {
            pt.us3 = parseFloat(this.value);
        }
        else {
            pt.s3 = parseFloat(this.value);
        }
        // Update m3Select
        var min = Math.max(0, Fanex.Round(n4 - parseFloat(this.value)));
        var max = is3Check.checked ? m4 : Math.min(m4, Fanex.Round(s4 - parseFloat(this.value)));
        var selected = parseFloat(m3Select.select.value);
        selected = ((!keepCurrent && !is3Check.checked) || selected < min || selected > max) ? max : selected;
        Fanex.PTVirtualSports.PT_SetSelect(m3Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
        m3Select.select.onchange(e, true);
    }

    is3Check.onclick = function (e) {
        // Update s3Select
        var min = Math.max(0, Fanex.Round(n4 - m4));
        var max = s4;
        var selected = this.checked ? pt.us3 : pt.s3;
        selected = (selected < min || selected > max) ? max : selected;
        Fanex.PTVirtualSports.PT_SetSelect(s3Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
        // Fire s3Select change
        s3Select.select.onchange(e, true);
    }

    m3Select.select.onchange = function (e, keepCurrent) {
        // Update n3Select
        var min = 0;
        var max = parseFloat(this.value);
        var selected = parseFloat(n3Select.select.value);
        selected = (selected < min || selected > max) ? max : selected;
        Fanex.PTVirtualSports.PT_SetSelect(n3Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    }

    // Save
    this.Save = function () {
        this.pt.is3 = is3Check.checked;
        if (this.pt.is3) {
            this.pt.us3 = parseFloat(s3Select.select.value);
            this.pt.s3 = 0;
        }
        else {
            this.pt.s3 = parseFloat(s3Select.select.value);
            this.pt.us3 = 0;
        }

        this.pt.m3 = parseFloat(m3Select.select.value);
        this.pt.n3 = parseFloat(n3Select.select.value);

        return {
            id: this.id,
            pt: this.pt
        }
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    owner.appendChild(box);

    return box;
};

// Agent edit Member
Fanex.PTVirtualSports.BetType.prototype.Render_2_1_1 = function (owner) { // a1
    box = document.createElement("DIV");

    if (typeof (this.pt.n2) == 'undefined') {
        this.pt.n2 = 0;
    }

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BettingLimitHeader";
    box.appendChild(header); // Header bet type name

    // a1
    var min = this.pt.n2;
    var max = this.pt.a2;
    var selected = this.pt.a1;
    var a1Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    a1Select.select.id = a1Select.select.name = "a1$" + this.suffixId;
    a1Select.className = "BetTypeSelect";
    a1Select.style.minWidth = "712px";
    box.appendChild(a1Select);

    this.Save = function () {
        this.pt.a1 = parseFloat(a1Select.select.value);
        return {
            id: this.id,
            pt: this.pt
        }
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    owner.appendChild(box);

    return box;
}

// Master edit Agent
Fanex.PTVirtualSports.BetType.prototype.Render_0_2_1 = function (owner) { // m2, a2, um2, im2, n2
    box = document.createElement("DIV");

    if (typeof (this.pt.m2) == 'undefined') {
        this.pt.m2 = this.pt.m3;
        this.pt.im2 = false;
        this.pt.um2 = 0;
        this.pt.n2 = 0;
        this.pt.a2 = 0;
    }

    var m3 = this.pt.m3;
    var n3 = this.pt.n3;
    var a4 = this.pt.a4;
    var m2 = this.pt.m2;
    var im2 = this.pt.im2;
    var um2 = im2 ? this.pt.um2 : 0;
    var n2 = this.pt.n2;
    var a2 = this.pt.a2;
    var m4 = this.pt.m4;

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BettingLimitHeader";
    box.appendChild(header); // Header bet type name

    // m2, um2
    var min = Math.max(0, Fanex.Round(n3 - a4));
    var max = Math.min(this.pt.m3, this.pt.m4);
    var selected = this.pt.im2 ? this.pt.um2 : this.pt.m2;
    var m2Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    m2Select.select.id = m2Select.select.name = "m2$" + this.suffixId;
    m2Select.className = "BetTypeSelect";
    if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        m2Select.style.minWidth = "204px";
    }
    else {
        m2Select.style.minWidth = "234px";
    }
    box.appendChild(m2Select);

    // im2
    var im2Check = document.createElement("INPUT");
    im2Check.type = "checkbox";
    im2Check.checked = im2Check.defaultChecked = this.pt.im2;
    im2Check.name = im2Check.id = "im2$" + this.suffixId;
    var im2Div = document.createElement("DIV");
    im2Div.className = "BetTypeCheckbox";
    im2Div.style.minWidth = "85px";
    if (!Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        im2Div.style.display = "none";
    }
    im2Div.appendChild(im2Check);
    box.appendChild(im2Div);

    // a2
    var min = Math.max(0, Fanex.Round(this.pt.n3 - (im2 ? um2 : m2)));
    var max = this.pt.im2 ? Math.min(this.pt.a4, this.pt.m3) : Math.min(this.pt.a4, Fanex.Round(this.pt.m3 - this.pt.m2));
    var selected = this.pt.a2;
    var a2Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    a2Select.select.id = a2Select.select.name = "a2$" + this.suffixId;
    a2Select.className = "BetTypeSelect";
    if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        a2Select.style.minWidth = "204px";
    }
    else {
        a2Select.style.minWidth = "234px";
    }
    box.appendChild(a2Select);

    // n2
    var min = 0;
    var max = this.pt.a2;
    var selected = this.pt.n2;
    var n2Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    n2Select.select.id = n2Select.select.name = "n2$" + this.suffixId;
    n2Select.className = "BetTypeSelect";
    if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        n2Select.style.minWidth = "204px";
    }
    else {
        n2Select.style.minWidth = "234px";
    }
    box.appendChild(n2Select);

    var pt = this.pt;
    m2Select.select.onchange = function (e, keepCurrent) {
        if (im2Check.checked) {
            pt.um2 = parseFloat(this.value);
        }
        else {
            pt.m2 = parseFloat(this.value);
        }

        var min = Math.max(0, Fanex.Round(n3 - parseFloat(this.value)));
        var max = im2Check.checked ? Math.min(a4, m3) : Math.min(a4, Fanex.Round(m3 - parseFloat(this.value)));
        var selected = parseFloat(a2Select.select.value);
        selected = ((!keepCurrent && !im2Check.checked) || selected < min || selected > max) ? max : selected;
        Fanex.PTVirtualSports.PT_SetSelect(a2Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
        a2Select.select.onchange(e, true);
    }

    im2Check.onclick = function (e) {
        // Update m2Select
        var min = Math.max(0, Fanex.Round(n3 - a4));
        var max = Math.min(m3, m4);
        var selected = this.checked ? pt.um2 : pt.m2;
        selected = (selected < min || selected > max) ? max : selected;
        Fanex.PTVirtualSports.PT_SetSelect(m2Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
        // Fire m2Select change
        m2Select.select.onchange(e, true);
    }

    a2Select.select.onchange = function (e, keepCurrent) {
        // Update n2Select
        var min = 0;
        var max = parseFloat(this.value);
        var selected = parseFloat(n2Select.select.value);
        selected = (selected < min || selected > max) ? max : selected;
        Fanex.PTVirtualSports.PT_SetSelect(n2Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    }

    this.Save = function () {
        this.pt.im2 = im2Check.checked;
        if (this.pt.im2) {
            this.pt.m2 = 0;
            this.pt.um2 = parseFloat(m2Select.select.value);
        }
        else {
            this.pt.m2 = parseFloat(m2Select.select.value);
            this.pt.um2 = 0;
        }
        this.pt.n2 = parseFloat(n2Select.select.value);
        this.pt.a2 = parseFloat(a2Select.select.value);

        return {
            id: this.id,
            pt: this.pt
        }
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    owner.appendChild(box);

    return box;
};

// Super edit Member
Fanex.PTVirtualSports.BetType.prototype.Render_4_1_1 = function (owner) { // s1, is1, us1, s1_max -> s4, s1_min -> n4, us1_max -> l4, us1_min -> 0, is3
    box = document.createElement("DIV");

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BettingLimitHeader";
    box.appendChild(header); // Header bet type name

    // s1, us1
    var min = this.pt.n4; // This is not a rule but s1_min already calculated and mapped to n4
    var max = this.pt.is1 ? this.pt.l4 : this.pt.s4; // This is not a rule but s1_max already calculated and mapped to s4, us1_max already mapped to l4
    var selected = this.pt.is1 ? this.pt.us1 : this.pt.s1;
    var s1Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    s1Select.select.id = s1Select.select.name = "s1$" + this.suffixId;
    if (!this.pt.is1 && this.pt.is3) {
        s1Select.select.disabled = true;
    }
    s1Select.className = "BetTypeSelect"; 
    s1Select.style.minWidth = "204px";
    box.appendChild(s1Select);

    // is1
    var is1Check = document.createElement("INPUT");
    is1Check.type = "checkbox";
    is1Check.checked = is1Check.defaultChecked = this.pt.is1;
    is1Check.name = "is1$" + this.suffixId;
    var is1Div = document.createElement("DIV");
    is1Div.className = "BetTypeCheckbox";
    is1Div.style.minWidth = "85px";
    is1Div.appendChild(is1Check);
    box.appendChild(is1Div);

    // m1
    var m1Label = document.createElement("DIV");
    m1Label.innerHTML = this.pt.m1;
    m1Label.className = "BetTypeLabel";
    m1Label.style.minWidth = "204px";
    box.appendChild(m1Label);

    // a1
    var a1Label = document.createElement("DIV");
    a1Label.innerHTML = this.pt.a1;
    a1Label.className = "BetTypeLabel";
    a1Label.style.minWidth = "204px";
    box.appendChild(a1Label);

    var s4 = this.pt.s4;
    var n4 = this.pt.n4;
    var l4 = this.pt.l4;
    var is3 = this.pt.is3;
    var is1 = this.pt.is1;
    var s1 = is1 ? 0 : this.pt.s1;
    var us1 = is1 ? this.pt.us1 : 0;

    var pt = this.pt;
    s1Select.select.onchange = function () {
        if (is1Check.checked) {
            pt.us1 = parseFloat(this.value);
        }
        else {
            pt.s1 = parseFloat(this.value);
        }
    }
     
    is1Check.onclick = function () {
        // Update s1Select
        var min = n4; // This is not a rule but s1_min already calculated and mapped to n4
        var max = this.checked ? l4 : s4; // This is not a rule but s1_max already calculated and mapped to s4, us1_max already mapped to l4
        var selected = this.checked ? pt.us1 : pt.s1;

        if (this.checked && is3) {
            s1Select.select.disabled = false;
            //selected = (selected < min || selected > max) ? max : selected;
        }
        else if (is3) {
            s1Select.select.disabled = true;
        }

        Fanex.PTVirtualSports.PT_SetSelect(s1Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    }

    this.Save = function () {
        this.pt.is1 = is1Check.checked;
        if (!this.pt.is1) {
            this.pt.s1 = parseFloat(s1Select.select.value);
            this.pt.us1 = 0;
        }
        else {
            this.pt.us1 = parseFloat(s1Select.select.value);
            //this.pt.s1 = 0;
        }

        return {
            id: this.id,
            pt: this.pt
        }
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    owner.appendChild(box);

    return box;
};

// Master edit Member
Fanex.PTVirtualSports.BetType.prototype.Render_3_1_1 = function (owner) { // m1, im1, um1, m1_max -> m3, m1_min -> n3, um1_max -> l3, um1_min = 0, im2
    box = document.createElement("DIV");

    if (typeof (this.pt.l3) == 'undefined') {
        this.pt.l3 = this.pt.m3;
    }

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BettingLimitHeader";
    box.appendChild(header); // Header bet type name

    // m1
    var min = this.pt.n3; // This is not a rule but m1_min already calculated and mapped to n3
    var max = this.pt.im1 ? this.pt.l3 : this.pt.m3; // This is not a rule but m1_max already calculated and mapped to m3, um1_max already mapped to l3
    var selected = this.pt.im1 ? this.pt.um1 : this.pt.m1;
    var m1Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    m1Select.select.id = m1Select.select.name = "m1$" + this.suffixId;
    if (!this.pt.im1 && this.pt.im2) {
        m1Select.select.disabled = true;
    }
    m1Select.className = "BetTypeSelect";
    if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        m1Select.style.minWidth = "308px";
    }
    else {
        m1Select.style.minWidth = "353px";
    }
    box.appendChild(m1Select);

    // im1
    var im1Check = document.createElement("INPUT");
    im1Check.type = "checkbox";
    im1Check.checked = im1Check.defaultChecked = this.pt.im1;
    im1Check.name = "im1$" + this.suffixId;
    var im1Div = document.createElement("DIV");
    im1Div.className = "BetTypeCheckbox";
    im1Div.style.minWidth = "85px";
    if (!Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        im1Div.style.display = "none";
    }
    im1Div.appendChild(im1Check);
    box.appendChild(im1Div);

    // a1
    var a1Label = document.createElement("DIV");
    a1Label.innerHTML = this.pt.a1;
    a1Label.className = "BetTypeLabel";
    if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
        a1Label.style.minWidth = "308px";
    }
    else {
        a1Label.style.minWidth = "354px";
    }
    box.appendChild(a1Label);

    var im1 = this.pt.im1;
    var n3 = this.pt.n3;
    var l3 = this.pt.l3;
    var m3 = this.pt.m3;
    var im2 = this.pt.im2;
    var im1 = this.pt.im1;
    var pt = this.pt;

    m1Select.select.onchange = function () {
        if (im1Check.checked) {
            pt.um1 = parseFloat(this.value);
        }
        else {
            pt.m1 = parseFloat(this.value);
        }
    }

    im1Check.onclick = function () {
        var min = n3; // This is not a rule but m1_min already calculated and mapped to n3
        var max = this.checked ? l3 : m3; // This is not a rule but m1_max already calculated and mapped to m3, um1_max already mapped to l3
        var selected = this.checked ? pt.um1 : pt.m1;
        
        if (this.checked && im2) {
            m1Select.select.disabled = false;
            //selected = (selected < min || selected > max) ? max : selected;
        }
        else if (im2) {
            m1Select.select.disabled = true;
        }

        Fanex.PTVirtualSports.PT_SetSelect(m1Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    }

    this.Save = function () {
        this.pt.im1 = im1Check.checked;
        if (!this.pt.im1) {
            this.pt.m1 = parseFloat(m1Select.select.value);
            this.pt.um1 = 0;
        }
        else {
            this.pt.um1 = parseFloat(m1Select.select.value);
            //this.pt.m1 = 0;
        }

        return {
            id: this.id,
            pt: this.pt
        }
    }

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    owner.appendChild(box);

    return box;
};

// Company edit Super
Fanex.PTVirtualSports.BetType.prototype.Render_0_4_1 = function (owner) {
    box = document.createElement("DIV");

    if (typeof (this.pt.l4) == 'undefined') {
        this.pt.l4 = 0.9;
    }

    // Bet type header
    var header = document.createElement("DIV");
    header.innerHTML = this.name;
    header.title = this.title;
    header.className = "BettingLimitHeader";
    box.appendChild(header); // Header bet type name

    // s4
    var min = 0;
    var max = this.pt.l4;
    var selected = this.pt.s4;
    var s4Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    s4Select.select.id = s4Select.select.name = "s4$" + this.suffixId;
    s4Select.className = "BetTypeSelect";
    s4Select.style.minWidth = "174px";
    box.appendChild(s4Select);

    s4Select.select.onchange = function () {
        var min = 0;
        var max = parseFloat(this.value);
        var selected = max;
        Fanex.PTVirtualSports.PT_SetSelect(m4Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
        m4Select.select.onchange();
        // Keep current value if possible
        selected = parseFloat(n4Select.select.value);
        selected = (selected > max) ? 0 : selected;
        Fanex.PTVirtualSports.PT_SetSelect(n4Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    };
    //m4
    var min = 0;
    var max = this.pt.s4;
    var selected = this.pt.m4;
    var m4Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    m4Select.select.id = m4Select.select.name = "m4$" + this.suffixId;
    m4Select.className = "BetTypeSelect";
    m4Select.style.minWidth = "174px";
    box.appendChild(m4Select);
    m4Select.select.onchange = function () {
        var max = parseFloat(this.value);
        var min = 0;
        var selected = max;
        Fanex.PTVirtualSports.PT_SetSelect(a4Select.select, min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    };
    //a4
    var min = 0;
    var max = this.pt.m4;
    var selected = this.pt.a4;
    var a4Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    a4Select.select.id = a4Select.select.name = "a4$" + this.suffixId;
    a4Select.className = "BetTypeSelect";
    a4Select.style.minWidth = "174px";
    box.appendChild(a4Select);

    //n4
    var min = 0;
    var max = this.pt.s4;
    var selected = this.pt.n4;
    var n4Select = Fanex.PTVirtualSports.PT_RenderSelect(min, max, Fanex.PTVirtualSports.PT_STEP, selected);
    n4Select.select.id = n4Select.select.name = "n4$" + this.suffixId;
    n4Select.className = "BetTypeSelect";
    n4Select.style.minWidth = "175px";
    box.appendChild(n4Select);

    box.id = "e$" + this.suffixId;
    box.className = "PTBox";
    owner.appendChild(box);

    this.Save = function () {
        this.pt.s4 = parseFloat(s4Select.select.value);
        this.pt.m4 = parseFloat(m4Select.select.value);
        this.pt.a4 = parseFloat(a4Select.select.value);
        this.pt.n4 = parseFloat(n4Select.select.value);
        return {
            id: this.id,
            pt: this.pt
        }
    }
    return box;
};