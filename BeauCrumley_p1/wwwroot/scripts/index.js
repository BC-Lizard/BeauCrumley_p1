
var signedInUser;

updateUserWithUrlParams();
updateLinksWithSignedInUser();

function updateUserWithUrlParams()
{
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const user = urlParams.get('user');
    try {
        signedInUser = JSON.parse(user);
        setSignedInText();
    } catch(err) {
        console.warn("Issue processing user. User not signed in.");
        //window.location.href = "login.html";
    }
}

function setInputErrorStyle(element)
{
    element.classList.add("input-error");
}
function removeInputErrorStyle(element)
{
    element.classList.remove("input-error");
}

function updateLinksWithSignedInUser()
{
    let elems = document.querySelectorAll("a");
    let href = "";
    for (let i = 0; i < elems.length; i++) {
        href = elems[i].getAttribute("href");
        if (href.indexOf("?") == -1) {
            hrefCropped = href;
        } else {
            hrefCropped = href.substr(0, href.indexOf("?"));
        }
        elems[i].setAttribute("href", `${hrefCropped}?user=${JSON.stringify(signedInUser)}`);
    }
}

function logout(returnToHome) {
    signedInUser = "";
    updateLinksWithSignedInUser();
    clearFields();
    if (returnToHome == true) {
        window.location.href = "index.html";
    }
}

function clearFields()
{
    let inputs = document.querySelectorAll(".form-field");
    for (let i = 0; i < inputs.length; i++) {
        inputs[i].querySelector("input").value = "";       
    }
}

function setSignedInText() {
    document.querySelector("#signed-in-as").innerHTML = `Signed in as: ${signedInUser.username}`;
    console.log("Signed in user: ", signedInUser);
}