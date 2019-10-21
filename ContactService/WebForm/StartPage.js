function validate(surname, surname) {
    ValidateName(document.getElementById("Name"));
    ValidateName(document.getElementById("Surname"));
    ValidateName(document.getElementById("Lastname"));

    $.ajax({
        type: "POST",
        url: "../ContactService.svc/InsertContact",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(DataObject())
      });
    
}


function DataObject(){
    return {
        Name: document.getElementById("Name").value,
        Surname: document.getElementById("Surname").value,
        Lastname: document.getElementById("Lastname").value,
        Sex: document.getElementById("datebox").value,
        Birthday: StringToDate( document.getElementById('Birthday').value),
        TaxId: document.getElementById("TaxId").value,
        PhoneNumber: document.getElementById("PhoneNumber").value,
        Post: document.getElementById("Post").value,
        Job: document.getElementById("Job").value,
    }
}


function ValidateName(Name){

    var strName= String( Name.value);
    var s1 = strName.charAt(0);
    strName= strName.charAt(0).toUpperCase() + strName.slice(1);
    Name.value=strName;
        if(strName.length>10){
            colorCanger.ToIncorrectColor(Name);
            return false;
        }
        else{
            colorCanger.ToCorrectColor(Name);
            return true;
        }
}
function ValidateSurname(Surname){

    var strSurname= String( Surname.value);
    strSurname= strSurname.charAt(0).toUpperCase() + strSurname.slice(1);
    Surname.value=strSurname;
        if(strSurname.length>15){
            colorCanger.ToIncorrectColor(Surname);
            return false;
        }
        else{
            colorCanger.ToCorrectColor(Surname);
            return true;
        }
}
function ValidateLastname(Lastname){

    var strLastname= String( Lastname.value);
    strLastname= strLastname.charAt(0).toUpperCase() + strLastname.slice(1);
    Lastname.value=strLastname;
        if(strLastname.length>15){
            colorCanger.ToIncorrectColor(Lastname);
            return false;
        }
        else{
            colorCanger.ToCorrectColor(Lastname);
            return true;
        }
}
function DateToString(date) {

    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
  
    var mm = date.getMonth() + 1;
    if (mm < 10) mm = '0' + mm;
  
    var yy = date.getFullYear();
    if (yy < 10) yy = '0' + yy;
  
    return dd + '.' + mm + '.' + yy;
  }
  function StringToDate(strDate) {

    var dd = strDate.slice(0,2);
    dd = Number(dd);

    var mm = strDate.slice(3,5);
    mm = Number(mm)-1;

    var yy = strDate.slice(6,10);
    yy = Number(yy);
  
    return new Date(yy,mm,dd);
  }

function ValidateBirthday(){

    var minDate= new Date(1900,0,1);

    var date;

    var options = {
        era: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long',
        timezone: 'UTC',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric'
      };
      
try{
   date=StringToDate(document.getElementById('Birthday').value);
  
   if(date< minDate)
   {
    colorCanger.ToIncorrectColor(document.getElementById('Birthday'));
    return false;
    }
    else{
        colorCanger.ToCorrectColor(document.getElementById('Birthday'));
    }
}catch(error){
    colorCanger.ToIncorrectColor(document.getElementById('Birthday'));
}
}

function ColorCanger(){
    this.ToCorrectColor=function(field){
        field.classList.remove('incorrect-field-data');
    }
    this.ToIncorrectColor=function(field){
        field.classList.remove('correct-field-data');
        field.classList.add('incorrect-field-data');
    }
}
var colorCanger = new ColorCanger();




function RefreshJob(){
  
    $.ajax({
        url: "http://localhost:8091/ContactService.svc/GetAllOrganizations",
        method: "POST",
        type: "json",
        success:  function(response) {
            $("#Job").attr('disabled', false);
            $.each(response,function(Id, Name)
            {
                var obj = JSON.parse(Name);
                $('#Job').append('<option value="'+obj.Id+'" >'+obj.Name+'</option>');
            });
        }
    });
}

function ObjectForSerchContact(){
    return {
        surname: String($("#IdSearch").value).split(',')[0],
        name: String($("#IdSearch").value).split(',')[1]
    }
}

function SearchContact(){
    alert("ghbd");
    $.ajax({
        type: "POST",
        url: "http://localhost:8091/ContactService.svc/GetContacts",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            surname: String($("#IdSearch").val()).split(',')[0],
            name: String($("#IdSearch").val()).split(',')[1]
        }
        ),
        success:  function(response) {
            $("#ContactsTable").attr('disabled', false);
            $.each(response,function(Id, obj)
            {
                var obj = JSON.parse(obj);
                $('#ContactsTable').append('<tr>'+
                                                '<th scope="row">1</th>'+
                                                '<td>'+obj[0].Name+'</td>'+
                                                '<td>'+obj[0].Surname+'</td>'+
                                                '<td>'+obj[0].Lastname+'</td>'+
                                                '<td>'+obj[0].Sex+'</td>'+
                                                '<td>'+obj[0].PhoneNumber+'</td>'+
                                                '<td>'+Date(obj[0].Birthday)+'</td>'+
                                                '<td>'+obj[0].TaxId+'</td>'+
                                                '<td>'+obj[0].Post+'</td>'+
                                                '<td>'+obj[0].Job.Name+'</td>'+
                                           '</tr>');
                //$('#ContactsTable').append('<option value="'+obj.Id+'" >'+obj.Name+'</option>');
            });
        }
      });
}
