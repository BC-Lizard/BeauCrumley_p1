
var signedInUser;

updateUserWithUrlParams();
updateLinksWithSignedInUser();

function updateUserWithUrlParams()
{
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const user = urlParams.get('user');
    signedInUser = JSON.parse(user);
    console.log(signedInUser);
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
        elems[i].setAttribute("href", `${href}?user=${JSON.stringify(signedInUser)}`);
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
    /*document.querySelector("#useremail").value = "";
    document.querySelector("#userpassword").value = "";*/
}