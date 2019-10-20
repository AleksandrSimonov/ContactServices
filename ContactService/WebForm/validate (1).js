
function validate(surname, surname) {
    $.ajax({
        type: "POST",
        url: "localhost/WcfService2/ContactService.svc/InsertContact",
        dataType: "JSON",
        data: DataArry()
      });
    
}
function DataArry(){
    return {
        Name: document.getElementById("Name").value,
        Surname: document.getElementById("Surname").value,
        Lastname: document.getElementById("Lastname").value,
        Sex: document.getElementById("datebox").value,
        Day: document.getElementById("Day").value,
        Month: document.getElementById("Month").value,
        Year: document.getElementById("Year").value,
        TaxId: document.getElementById("TaxId").value,
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

function ValidateDay(){
    var Day = document.getElementById('Day');
    if(Day.value=="")
        return false;
    if(isNaN(Number(Day.value))||isNaN(parseInt(Day.value))||Day.value>31){
        colorCanger.ToIncorrectColor(document.getElementById('Day'));
        return false;
    }
    colorCanger.ToCorrectColor(document.getElementById('Day'));
    return true;
}
function ValidateMonth(){
    var Month = document.getElementById('Month');
    if(Month.value=="")
        return false;
    if(isNaN(Number(Month.value))||isNaN(parseInt(Month.value))||Month.value>12){
        colorCanger.ToIncorrectColor(document.getElementById('Month'));
        return false;
    }
    colorCanger.ToCorrectColor(document.getElementById('Month'));
    return true;
}
function ValidateYear(){
    var Year = document.getElementById('Year');
    if(Year.value=="")
        return false;
    if(isNaN(Number(Year.value))||isNaN(parseInt(Year.value))){
        colorCanger.ToIncorrectColor(document.getElementById('Year'));
        return false;
    }
    colorCanger.ToCorrectColor(document.getElementById('Year'));
    return true;
}

function ValidateDate(){

    var minDate= new Date(1900,0,1);

    


 if(ValidateDay()&&ValidateMonth()&&ValidateYear()){

    var nDay= Number( document.getElementById('Day').value);
    var nMonth= Number( document.getElementById('Month').value);
    var nYear= Number( document.getElementById('Year').value);

    var date=new Date(nYear,nMonth-1,nDay);

    if(date.getDate()!=nDay){
        colorCanger.ToIncorrectColor(document.getElementById('Day'));
        return;
    }
    if(date.getMonth()!=nMonth-1){
        colorCanger.ToIncorrectColor(document.getElementById('Month'));
        return;
    }
    if(date.getFullYear()!=nYear){
        colorCanger.ToIncorrectColor(document.getElementById('Year'));
        return;
    }
        if(date< minDate){
            colorCanger.ToIncorrectColor(document.getElementById('Day'));
            colorCanger.ToIncorrectColor(document.getElementById('Month'));
            colorCanger.ToIncorrectColor(document.getElementById('Year'));
            return false;
        }
        else{
            colorCanger.ToCorrectColor(document.getElementById('Day'));
            colorCanger.ToCorrectColor(document.getElementById('Month'));
            colorCanger.ToCorrectColor(document.getElementById('Year'));
            return true;
        }
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

 
 // оформить в виде функций конструкоров.


