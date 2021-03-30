
addLoginEventListeners();

var signedInUser;

function addLoginEventListeners() {
    document.querySelector('#login-button').addEventListener('click', attemptLogin)
}

function attemptLogin()
{
    let user_email_input = document.querySelector("#useremail").value;
    let user_password_input = document.querySelector("#userpassword").value;

    fetch(`api/Users/${user_email_input}`)
        .then(data => {
            return data.text();
        })
        .then(salt => {
            console.log("Response when getting user Salt: " + salt);
            let passed = checkValidSalt(salt);
            if (passed) {
                let hashedPass = hashPasswordUsingSalt(salt, user_password_input);
                signedInUser = loginWithHash(user_email_input, hashedPass);
            } else {
                console.log("Failed to verify user.");
            }
        });
}

function loginWithHash(email, pass)
{
    const response = postLoginCreds(email, pass)
        .then(data => {
            console.log("Response to login attempt with email and password.");
            console.log(data);
            if (data.accountNo == 0) {
                setLoginError();
            } else {
                clearFields();
                clearLoginError();
                setLoginSuccess();
            }
            return data;
        });
    return response;
}

function setLoginError()
{
    //change page styles to show login error
}
function clearLoginError()
{
    //revert styles if login error gone
}
function setLoginSuccess()
{
    //set page to logged-in state
    //unlock features that need user signed in
}


function checkValidSalt(str)
{
    if (str === "_") {
        return false;
    } else {
        return true;
    }
}

function clearFields()
{
    document.querySelector("#useremail").value = "";
    document.querySelector("#userpassword").value = "";
}

async function postLoginCreds(email, pass)
{
    const response = await fetch(`api/Users/${pass}/${email}`, {
        method: 'POST'
    });
    let user = response.json();
    console.log("POST Login Creds(user): ", user);
    return user;
}

function hashPasswordUsingSalt(salt, password)
{
    let saltedPassword = password + salt;
    return SHA256(saltedPassword);
}