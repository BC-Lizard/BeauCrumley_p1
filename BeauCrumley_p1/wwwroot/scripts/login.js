
addLoginEventListeners();

function addLoginEventListeners() {
    document.querySelector('#login-button').addEventListener('click', attemptLogin);
}

function attemptLogin()
{
    let user_email_input = document.querySelector("#useremail").value;
    let user_password_input = document.querySelector("#userpassword").value;

    if (checkLoginFields() == true) {
        fetch(`api/Users/${user_email_input}`)
            .then(data => {
                return data.text();
            })
            .then(salt => {
                console.log("Response when getting user Salt: " + salt);
                let passed = checkValidSalt(salt);
                if (passed) {
                    let hashedPass = hashPasswordUsingSalt(salt, user_password_input);
                    loginWithHash(user_email_input, hashedPass);
                } else {
                    console.log("Failed to verify user.");
                }
            });
    } else {
        console.warn("Login error. Please make sure fields are filled.");
    }

}

function loginWithHash(email, pass)
{
    const response = postLoginCreds(email, pass)
        .then(data => {
            console.log("Response to login attempt with email and password.");
            console.log(data);
            signedInUser = data;
            updateLinksWithSignedInUser();
            if (data.accountNo == 0) {
                console.warn("Login not successful. Check email and password are correct.");
            } else {
                clearFields();
                document.querySelector("#home-link").click();
            }
            return data;
        });
}

function checkLoginFields()
{
    let noErrorsPresent = true;
    if (document.querySelector("#useremail").value == "") {
        noErrorsPresent = false;
        setInputErrorStyle(document.querySelector("#useremail"));
    }
    if (document.querySelector("#userpassword").value == "") {
        noErrorsPresent = false;
        setInputErrorStyle(document.querySelector("#userpassword"));
    }
    return noErrorsPresent;
}


function checkValidSalt(str)
{
    if (str === "_") {
        return false;
    } else {
        return true;
    }
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