
addRegisterEventListeners();

// EMAIL | USERNAME | FIRSTNAME | LASTNAME | PASSWORD | PHONE
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
    console.log("Attempting registration. . .");
    if (checkForErrors() == false) {
        console.log("No errors. Sending Request.");
        let newSalt = generateSalt();
        let newHashedPassword = generateHashedPassword(newSalt);
        let newUserDataString = generateUserDataString(newSalt, newHashedPassword);
        fetch(`api/Users/${newUserDataString}`, {method: 'POST'})
            .then(response => response.text())
            .then(response => console.log(response));
            logout(false);
    } else {
        console.warn(". . .Input errors detected. Canceling registration attempt.");
    }
}

function generateSalt()
{
    let seed = Math.floor((Math.random() * 999999999) + 100000000).toString();
    return SHA256(seed);
}
function generateHashedPassword(salt)
{
    return SHA256(registerFormInputs[4].value + salt);
}
function generateUserDataString(salt, password)
{
    //FirstName-LastName-Username-PasswordSalt-HashedPassword-Phonenumber-Email
    let str = "";
    str += registerFormInputs[2].value + "-";
    str += registerFormInputs[3].value + "-";
    str += registerFormInputs[1].value + "-";
    str += salt + "-";
    str += password + "-";
    str += registerFormInputs[5].value + "-";
    str += registerFormInputs[0].value;

    console.log(str);

    return str;
}

function checkForErrors()
{
    let errorsExist = false;
    if (emailIsValid() == false) {
        errorsExist = true;
    }
    if (usernameIsValid() == false) {
        errorsExist = true;
    }
    if (fnameIsValid() == false) {
        errorsExist = true;
    }
    if (lnameIsValid() == false) {
        errorsExist = true;
    }
    if (passwordIsValid() == false) {
        errorsExist = true;
    }
    if (phoneIsValid() == false) {
        errorsExist = true;
    }
    return errorsExist;
}

function emailIsValid()
{
    let mailformat = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    console.log(registerFormInputs[0].value);
    if (!registerFormInputs[0].value.match(mailformat)) {
        console.warn("email bad");
        setInputErrorStyle(registerFormInputs[0]);
        return false;
    } else {
        console.log("email good");
        removeInputErrorStyle(registerFormInputs[0]);
        return true;
    }
}
function usernameIsValid()
{
    let usernameFormat = /^(?=.{8,20}$)(?![_])(?!.*[_]{2})[a-zA-Z0-9_]+(?<![_])$/;
    if (!registerFormInputs[1].value.match(usernameFormat)) {
        console.warn("username bad");
        setInputErrorStyle(registerFormInputs[1]);
        return false;
    } else {
        console.log("username good");
        removeInputErrorStyle(registerFormInputs[1]);
        return true;
    }
}
function fnameIsValid()
{
    let fnameFormat = /^(?=.{2,20}$)[a-zA-Z]+$/;
    if (!registerFormInputs[2].value.match(fnameFormat)) {
        console.warn("first name bad");
        setInputErrorStyle(registerFormInputs[2]);
        return false;
    } else {
        console.log("first name good");
        removeInputErrorStyle(registerFormInputs[2]);
        return true;
    }
}
function lnameIsValid()
{
    let lnameFormat = /^(?=.{2,20}$)[a-zA-Z]+$/;
    if (!registerFormInputs[3].value.match(lnameFormat)) {
        console.warn("last name bad");
        setInputErrorStyle(registerFormInputs[3]);
        return false;
    } else {
        console.log("last name good");
        removeInputErrorStyle(registerFormInputs[3]);
        return true;
    }
}
function passwordIsValid()
{
    let passwordFormat = /^(?=.{8,100}$)/;
    if (!registerFormInputs[4].value.match(passwordFormat)) {
        console.warn("password bad");
        setInputErrorStyle(registerFormInputs[4]);
        return false;
    } else {
        console.log("password good");
        removeInputErrorStyle(registerFormInputs[4]);
        return true;
    }
}
function phoneIsValid()
{
    if (registerFormInputs[5].value.length != 10) {
        console.warn("phone bad");
        setInputErrorStyle(registerFormInputs[5]);
        return false;
    } else {
        console.log("phone good");
        removeInputErrorStyle(registerFormInputs[5]);
        return true;
    }
}