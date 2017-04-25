if (typeof (Fanex) == 'undefined') { // Require Fanex namespace
	Fanex = {};
}
if (typeof (Fanex.PTVirtualSports) == 'undefined') { // Require Fanex.PTVirtualSports namespace
	alert('Require Fanex.PTVirtualSports namespace');
}

Fanex.PTVirtualSports.PTGrid.prototype.Render_0_3_1 = function (owner) {
	var box = document.createElement("DIV");
	var labelBox = document.createElement("DIV");
	//labelBox.innerHTML = Language.VIRTUAL_SPORTS;
	labelBox.className = "BettingLimitHeader";
	box.appendChild(labelBox);
	var superPTHeader = document.createElement("DIV");
	superPTHeader.innerHTML = Language.SUPER_PT;
	superPTHeader.className = "BetTypeHeader";
	superPTHeader.style.minWidth = "204px";
	box.appendChild(superPTHeader);
	var isAutoHeader = document.createElement("DIV");
	isAutoHeader.innerHTML = Language.IS_AUTO;
	isAutoHeader.className = "BetTypeHeader";
	isAutoHeader.style.minWidth = "85px";
	box.appendChild(isAutoHeader);
	var masterMaxPTHeader = document.createElement("DIV");
	masterMaxPTHeader.innerHTML = Language.MASTER_MAX_PT;
	masterMaxPTHeader.className = "BetTypeHeader";
	masterMaxPTHeader.style.minWidth = "204px";
	box.appendChild(masterMaxPTHeader);
	var masterMinPTHeader = document.createElement("DIV");
	masterMinPTHeader.innerHTML = Language.MASTER_MIN_PT;
	masterMinPTHeader.className = "BetTypeHeader";
	masterMinPTHeader.style.minWidth = "204px";
	box.appendChild(masterMinPTHeader);
	owner.appendChild(box);
	return box;
}

Fanex.PTVirtualSports.PTGrid.prototype.Render_2_1_1 = function (owner) {
	var box = document.createElement("DIV");
	var labelBox = document.createElement("DIV");
	//labelBox.innerHTML = Language.VIRTUAL_SPORTS;
	labelBox.className = "BettingLimitHeader";
	box.appendChild(labelBox);
	var agentPTHeader = document.createElement("DIV");
	agentPTHeader.innerHTML = Language.AGENT_PT;
	agentPTHeader.className = "BetTypeHeader";
	agentPTHeader.style.minWidth = "710px";
	box.appendChild(agentPTHeader);
	return box;
}

Fanex.PTVirtualSports.PTGrid.prototype.Render_0_2_1 = function (owner) {
	var box = document.createElement("DIV");
	var labelBox = document.createElement("DIV");
	//labelBox.innerHTML = Language.VIRTUAL_SPORTS;
	labelBox.className = "BettingLimitHeader";
	box.appendChild(labelBox);
	var masterPTHeader = document.createElement("DIV");
	masterPTHeader.innerHTML = Language.MASTER_PT;
	masterPTHeader.className = "BetTypeHeader";
	if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
		masterPTHeader.style.minWidth = "204px";
	}
	else {
		masterPTHeader.style.minWidth = "234px";
	}
	box.appendChild(masterPTHeader);
	var isAutoHeader = document.createElement("DIV");
	isAutoHeader.innerHTML = Language.IS_AUTO;
	isAutoHeader.className = "BetTypeHeader";
	isAutoHeader.style.minWidth = "85px";
	if (!Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
		isAutoHeader.style.display = "none";
	}
	box.appendChild(isAutoHeader);
	var agentMaxPTHeader = document.createElement("DIV");
	agentMaxPTHeader.innerHTML = Language.AGENT_MAX_PT;
	agentMaxPTHeader.className = "BetTypeHeader";
	if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
		agentMaxPTHeader.style.minWidth = "204px";
	}
	else {
		agentMaxPTHeader.style.minWidth = "234px";
	}
	box.appendChild(agentMaxPTHeader);
	var agentMinPTHeader = document.createElement("DIV");
	agentMinPTHeader.innerHTML = Language.AGENT_MIN_PT;
	agentMinPTHeader.className = "BetTypeHeader";
	if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
		agentMinPTHeader.style.minWidth = "204px";
	}
	else {
		agentMinPTHeader.style.minWidth = "234px";
	}
	box.appendChild(agentMinPTHeader);
	owner.appendChild(box);
	return box;
}

Fanex.PTVirtualSports.PTGrid.prototype.Render_4_1_1 = function (owner) {
	var box = document.createElement("DIV");
	var labelBox = document.createElement("DIV");
	//labelBox.innerHTML = Language.VIRTUAL_SPORTS;
	labelBox.className = "BettingLimitHeader";
	box.appendChild(labelBox);
	var superPTHeader = document.createElement("DIV");
	superPTHeader.innerHTML = Language.SUPER_PT;
	superPTHeader.className = "BetTypeHeader";
	superPTHeader.style.minWidth = "204px";
	box.appendChild(superPTHeader);
	var isAutoHeader = document.createElement("DIV");
	isAutoHeader.innerHTML = Language.IS_AUTO;
	isAutoHeader.className = "BetTypeHeader";
	isAutoHeader.style.minWidth = "85px";
	box.appendChild(isAutoHeader);
	var masterPTHeader = document.createElement("DIV");
	masterPTHeader.innerHTML = Language.MASTER_PT;
	masterPTHeader.className = "BetTypeHeader";
	masterPTHeader.style.minWidth = "204px";
	box.appendChild(masterPTHeader);
	var agentPTHeader = document.createElement("DIV");
	agentPTHeader.innerHTML = Language.AGENT_PT;
	agentPTHeader.className = "BetTypeHeader";
	agentPTHeader.style.minWidth = "204px";
	box.appendChild(agentPTHeader);
	owner.appendChild(box);
	return box;
}

Fanex.PTVirtualSports.PTGrid.prototype.Render_3_1_1 = function (owner) {
	var box = document.createElement("DIV");
	var labelBox = document.createElement("DIV");
	//labelBox.innerHTML = Language.VIRTUAL_SPORTS;
	labelBox.className = "BettingLimitHeader";
	box.appendChild(labelBox);
	var masterPTHeader = document.createElement("DIV");
	masterPTHeader.innerHTML = Language.MASTER_PT;
	masterPTHeader.className = "BetTypeHeader";
	if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
	    masterPTHeader.style.minWidth = "308px";
	}
	else {
	    masterPTHeader.style.minWidth = "353px";
	}
	box.appendChild(masterPTHeader);
	var isAutoHeader = document.createElement("DIV");
	isAutoHeader.innerHTML = Language.IS_AUTO;
	isAutoHeader.className = "BetTypeHeader";
	isAutoHeader.style.minWidth = "86px";
	if (!Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
		isAutoHeader.style.display = "none";
	}
	box.appendChild(isAutoHeader);
	var agentPTHeader = document.createElement("DIV");
	agentPTHeader.innerHTML = Language.AGENT_PT;
	agentPTHeader.className = "BetTypeHeader";
	if (Fanex.PTVirtualSports.CURRENT_CONTEXT.ia3) {
		agentPTHeader.style.minWidth = "308px";
	}
	else {
		agentPTHeader.style.minWidth = "354px";
	}
	box.appendChild(agentPTHeader);
	owner.appendChild(box);
	return box;
}

Fanex.PTVirtualSports.PTGrid.prototype.Render_0_4_1 = function (owner) {
	var box = document.createElement("DIV");

	var labelBox = document.createElement("DIV");
	//labelBox.innerHTML = Language.VIRTUAL_SPORTS;
	labelBox.className = "BettingLimitHeader";
	box.appendChild(labelBox);
	//s4
	var superMaxPTHeader = document.createElement("DIV");
	superMaxPTHeader.innerHTML = Language.SUPER_MAX_PT;
	superMaxPTHeader.className = "BetTypeHeader";
	superMaxPTHeader.style.minWidth = "174px";
	box.appendChild(superMaxPTHeader);
	//m4
	var masterMaxPTHeader = document.createElement("DIV");
	masterMaxPTHeader.innerHTML = Language.MASTER_MAX_PT;
	masterMaxPTHeader.className = "BetTypeHeader";
	masterMaxPTHeader.style.minWidth = "174px";
	box.appendChild(masterMaxPTHeader);
	//a4
	var agentMaxPTHeader = document.createElement("DIV");
	agentMaxPTHeader.innerHTML = Language.AGENT_MAX_PT;
	agentMaxPTHeader.className = "BetTypeHeader";
	agentMaxPTHeader.style.minWidth = "174px";
	box.appendChild(agentMaxPTHeader);
	//n4
	var superMinPTHeader = document.createElement("DIV");
	superMinPTHeader.innerHTML = Language.SUPER_MIN_PT;
	superMinPTHeader.className = "BetTypeHeader";
	superMinPTHeader.style.minWidth = "175px";
	box.appendChild(superMinPTHeader);

	owner.appendChild(box);
	return box;
}