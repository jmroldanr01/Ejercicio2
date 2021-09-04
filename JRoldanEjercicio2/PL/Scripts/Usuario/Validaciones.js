function EmptyInput() {
  
    var password = document.getElementById("txtPassword").value;
    var ApellidoPaterno = document.getElementById("txtLastName").value;
    var ApellidoMaterno = document.getElementById("txtSurname").value;
    var Nombre = document.getElementById("txtName").value;
    var Email = document.getElementById("txtEmail").value;
    var UserName = document.getElementById("txtUserName").value;
    var Password = document.getElementById("txtPassword").value;
    var ConfirmPassword = document.getElementById("txtConfirmPassword").value;
    var Parent = document.getElementById("txtParent").value;
    var Status = document.getElementById("txtStatus").value;
    var Role = document.getElementById("txtRol").value;


    if (password == "" || ApellidoPaterno == "" || ApellidoMaterno == "" ||
        Nombre == "" || Email == "" || UserName == "" || ConfirmPassword == "" ||
        Password=="" || Parent=="" || Status=="" || Role=="") {
         
        return false;
    }
    return true;
}
function ValidatePassword() {
    var password = document.getElementById("txtPassword").value;
    var confirmPassword = document.getElementById("txtConfirmPassword").value;
    var lblError = document.getElementById("lblErrorPassword");
    lblError.innerHTML = "";
    if (password != confirmPassword) {
        $('#txtPassword').css({ "border": "1px solid red" });
        $('#txtConfirmPassword').css({ "border": "3px solid red" });
        lblError.innerHTML = "La Contraseña no coincide";
        return false;
    }
    $('#txtPassword').css({ "border": "1px solid green" });
    $('#txtConfirmPassword').css({ "border": "3px solid green" });
    return true;
}

function ValidateEmail(e) {
    var email = document.getElementById("txtEmail").value;
    var lblError = document.getElementById("lblEmailError");
    lblError.innerHTML = "";
    var expr = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;;
    if (!expr.test(email)) {
        $('#txtEmail').css({ "border": "1px solid red" });
        lblError.innerHTML = "Correo No Valido";
        return false;
    }
    $('#txtEmail').css({ "border": "1px solid green" });
    lblError.innerHTML = "";
    return true;
}  
function SoloLetras(e) {
    var key = e.keyCode || e.which;
    var letra = String.fromCharCode(key).toLowerCase();
    var caracter = e.key;
    if (!/^[a-zA-Z]*$/g.test(letra)) {
        document.getElementById(e.target.id).style.borderColor = "Red";
        alert("Solo se aceptan letras");
        return false;

    }
    else {
        document.getElementById(e.target.id).style.borderColor = "Green";
        return true;
    }
}    

function SoloNumeros(e) {
    var specialKeys = new Array();
    specialKeys.push(8);
    var keyCode = e.which ? e.which : e.keyCode
    var lblError = document.getElementById(e.target.id);
    lblError.innerHTML = "";
    var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
    if (!ret )
    {
        alert("Solo numeros");
        lblError.innerHTML = "Solo Numeros";
        return false;
           
    }

    return ret;
}
function Validate() {
        var email = ValidateEmail();
        var password = ValidatePassword();
        var Vacio = EmptyInput();

        if (email == true && password == true && Vacio==true) {
            
            return true;
        }
        alert("Un campo no esta validado y/o esta vacio");
        return false;
    }