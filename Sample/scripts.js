// return to the top of the page
function moveToTop() {
  document.body.scrollTop = 0; // For Chrome, Safari and Opera
  document.documentElement.scrollTop = 0; // For IE and Firefox
}

// swap the cover letter and resume proper
function swapCover(direction) {

  function setAnimationAndPosition(id, shown) {
    let temp = document.getElementById(id);
    temp.style.animationName = (shown) ? "swap-in" : "swap-out";
    temp.style.animationDuration = "1s";
    temp.style.left = (shown) ? "0" : "-100%";
    temp.getElementsByClassName("controls")[0].style.display = (shown) ? "block" : "none";
    console.log((shown) ? "swap-in, 0, block" : "swap-out, -100%, none");
  }
  
  function swap(fromID, toID) {
    console.log("swap from " + fromID + " to " + toID);
    setAnimationAndPosition(fromID, false);
    setAnimationAndPosition(toID, true);
  }

  (direction) ? swap("cover", "resume") : swap("resume", "cover");
  moveToTop();
                
}
                        
// expand and contract the controls dropdown
function expandControls(id, direction) {
  
  function swap(id, fromClass, toClass) {
    let controls = document.getElementById(id);
    controls.getElementsByClassName(fromClass)[0].style.display = "none";
    controls.getElementsByClassName(toClass)[0].style.display = "block";
  }
  
  console.log(id);
  direction ? swap(id, "expand-button", "controls-buttons") : swap(id, "controls-buttons", "expand-button");
              
}

window.onbeforeunload = function(){
    expandControls('resume-controls', false);
    swapCover(false);
    console.log("reload");
}