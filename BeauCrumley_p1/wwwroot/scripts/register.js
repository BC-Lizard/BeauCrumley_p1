
addRegisterEventListeners();

//if any of the error flags array are true, there is an error in that field
// EMAIL | USERNAME | FIRSTNAME | LASTNAME | PASSWORD | PHONE
let registerFormErrorFlags = [false, false, false, false, false, false];
let registerFormInputs = [
    document.querySelector("#registeremail"),
    document.querySelector("#registeruser"),
    document.querySelector("#registerfname"),
    document.querySelector("#registerlname"),
    document.querySelector("#registerpass"),
    document.querySelector("#registerphone")
];

function addRegisterEventListeners()
{
    document.querySelector("#register-button").addEventListener("click", attemptRegister);
}

function attemptRegister()
{
    if (checkForErrors() == false)
    {
        //no input errors detected, make api request
    }
}

function checkForErrors()
{
    let errorsExist = false;
    if (emailIsValid == false) {
        errorsExist = true;
    }
    if (usernameIsValid == false) {
        errorsExist = true;
    }
    if (fnameIsValid == false) {
        errorsExist = true;
    }
    if (lnameIsValid == false) {
        errorsExist = true;
    }
    if (passwordIsValid == false) {
        errorsExist = true;
    }
    if (phoneIsValid == false) {
        errorsExist = true;
    }
    return errorsExist;
}

function emailIsValid()
{
    //check that input follows rules of field. Update styles if not.
    return true;
}
function usernameIsValid()
{
    //check that input follows rules of field. Update styles if not.
    return true;
}
function fnameIsValid()
{
    //check that input follows rules of field. Update styles if not.
    return true;
}
function lnameIsValid()
{
    //check that input follows rules of field. Update styles if not.
    return true;
}
function passwordIsValid()
{
    //check that input follows rules of field. Update styles if not.
    return true;
}
function phoneIsValid()
{
    //check that input follows rules of field. Update styles if not.
    return true;
}