function validate(surname, surname) {
    ValidateName(document.getElementById("Name"));
    ValidateName(document.getElementById("Surname"));
    ValidateName(document.getElementById("Lastname"));

    $.ajax({
        type: "POST",
        url: "../ContactService.svc/InsertContact",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(DataObject()),
        success:  function(response) {
           
            $.each(response,function(Id, obj)
            {
                alert(obj);
            }
            );
        }
      });
    
}

function UpdateContact() {
    ValidateName(document.getElementById("Name"));
    ValidateName(document.getElementById("Surname"));
    ValidateName(document.getElementById("Lastname"));

    $.ajax({
        type: "POST",
        url: "../ContactService.svc/UpdateContact",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(DataObject()),
        success:  function(response) {
           
            $.each(response,function(Id, obj)
            {
                alert(obj);
            }
            );
        }
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

    date = new Date(date);

    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
  
    var mm = date.getMonth() + 1;
    if (mm < 10) mm = '0' + mm;
  
    var yyyy = date.getFullYear();
  
    return dd + '.' + mm + '.' +yyyy;
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
        url: "../ContactService.svc/GetAllOrganizations",
        method: "POST",
        type: "json",
        success:  function(response) {
            $("#Job").attr('disabled', false);
            $.each(response,function(Id, obj)
            {
                var obj = JSON.parse(obj);
                $('#Job [value!=-1]').remove();
                for(let i=0; i< obj.length; i++){
                $('#Job').append('<option value="'+obj[i].Id+'" >'+obj[i].Name+'</option>');
            }
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
    $.ajax({
        type: "POST",
        url: "../ContactService.svc/GetContacts",
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

                $('#ContactsTable tbody').remove();
              
                for(let i=0; i<obj.length; i++){
                    
                $('#ContactsTable').append('<tr id= "'+obj[i].TaxId+'" data-toggle="popover" data-content="удалить контакт">'+
                                                '<th scope="row">'+(i+1)+'</th>'+
                                                '<td>'+obj[i].Name+'</td>'+
                                                '<td>'+obj[i].Surname+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].Lastname)+'</td>'+
                                                '<td>'+CodeToSex(obj[i].Sex)+'</td>'+
                                                '<td>'+DateToString( obj[i].Birthday)+'</td>'+
                                                '<td>'+obj[i].TaxId+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].PhoneNumber)+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].Post)+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].Job.Name)+'</td>'+
                                                '<td> <button id= "'+obj[i].TaxId+'" type="button" class="btn btn-outline-primary" onclick="UpdateContactForm(this)">редактировать контакт</button> </td>'+
                                                '<td> <button onclick="DeleteContact(this)" id= "'+obj[i].TaxId+'" type="button" class="btn btn-outline-danger">удалить контакт</button> </td>'+
                                           '</tr>');
                                        
                                       
                              
                }
                alert("По запросу найдено "+obj.length+" контактов(а)");
                //$('#ContactsTable').append('<option value="'+obj.Id+'" >'+obj.Name+'</option>');
            });
        }
      });
}
function AddNewContactForm(){
    $('#Name').val("")
    $('#Surname').val("")
    $('#Lastname').val("")
    $('#Sex').val("")
    $('#Birthday').val("")
    $('#TaxId').val("")
    $('#SurPhonwNumber').val("")
    $('#Post').val("")
    $('#Job').val("")
    $('#TaxId')[0].readOnly=false;
    $('#exampleModalCenter').modal();
    $('#ContactFormTitle')[0].innerText="Добавьте новый контакт";
    $('#ContactToDb')[0].onclick = validate;
}
function UpdateContactForm(options){
    $('#exampleModalCenter').modal();

    $('#ContactFormTitle')[0].innerText="Введите изменения в контакт";
    $('#Name').val( $('#'+options.id+' td')[0].innerHTML);
    $('#Surname').val( $('#'+options.id+' td')[1].innerHTML);
    $('#Lastname').val( $('#'+options.id+' td')[2].innerHTML);
    $('#Sex').val( $('#'+options.id+' td')[3].innerHTML);
    $('#Birthday').val( $('#'+options.id+' td')[4].innerHTML);
    $('#TaxId').val( $('#'+options.id+' td')[5].innerHTML);
    $('#TaxId')[0].readOnly=true;
    $('#SurPhonwNumber').val( $('#'+options.id+' td')[6].innerHTML);
    $('#Post').val( $('#'+options.id+' td')[7].innerHTML);
    $('#Job').val( $('#'+options.id+' td')[8].innerHTML);

    $('#ContactToDb')[0].onclick = UpdateContact;

}
function InizializeForm(){
  
}
function DeleteContact(contact){
  
    $.ajax({
        type: "POST",
        url: "../ContactService.svc/DeleteContact",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            taxId: contact.id
        }),
        success: function(){
            $('#ContactsTable [id= '+contact.id+']').remove();
            alert("контакт успешно удален");
        }
      });
     
      
}

function EditContact(contact){
   $('#ContactsTable [id= '+contact.id+'] td').each(function(id,col){
      col.contenteditable="true"})    
}

function CodeToSex(codeOfSex){
    if(Number(codeOfSex)==0)
        return "Муж.";
    if(Number(codeOfSex)==1)
        return "Жен.";
        else
        return "ошибка";
}
function NullToUndefind(field){
    if(field==null)
        return "не указано";
    else 
        return field;
} 

function ShowContactForm(){
    document.getElementById("ContactForm").hidden=false;

}