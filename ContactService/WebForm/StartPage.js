function InsertOrUpdateContact() {

    if(
        ValidateName(document.getElementById("Name"))&&
        ValidateSurname(document.getElementById("Surname"))&&
        ValidateLastname(document.getElementById("Lastname"))&&
        ValidateBirthday(document.getElementById("Birthday"))&&
        ValidateITN(document.getElementById("ITN"))&&
        ValidatePhoneNumber(document.getElementById("PhoneNumber"))
    ){

        $.ajax({
            type: "POST",
            url: "../ContactService.svc/InsertOrUpdateContact",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(DataObject()),
            success:  function(response) {
            
                $.each(response,function(Id, obj)
                {
                    alert(obj);
                }
                ),
                SearchContact();
            }
        });
    }
}

function DataObject(){
    return {
        Id: document.getElementById("ContactId").value,
        Name: document.getElementById("Name").value,
        Surname: document.getElementById("Surname").value,
        Lastname: document.getElementById("Lastname").value,
        Sex: document.getElementById("datebox").value,
        Birthday: StringToDate( document.getElementById('Birthday').value),
        ITN: document.getElementById("ITN").value,
        PhoneNumber: document.getElementById("PhoneNumber").value,
        Post: document.getElementById("Post").value,
        Job: document.getElementById("Job").value,
    }
}

function ValidateName(Name){

    var strName= String(Name.value);
    strName= strName.charAt(0).toUpperCase() + strName.slice(1);
    Name.value=strName;

    if(strName.length>15){
        Name.classList.add('is-invalid');
        Name.classList.remove('is-valid');
        return false;
    }
    else{
        Name.classList.remove('is-invalid');
        Name.classList.add('is-valid');
        return true;
    }
}
function ValidateSurname(Surname){

    var strSurname= String( Surname.value);
    strSurname= strSurname.charAt(0).toUpperCase() + strSurname.slice(1);
    Surname.value=strSurname;

        if(strSurname.length>15){
            Surname.classList.add('is-invalid');
            Surname.classList.remove('is-valid');
            return false;
        }
        else{
            Surname.classList.remove('is-invalid');
            Surname.classList.add('is-valid');
            return true;
        }
}
function ValidateLastname(Lastname){

    var strLastname= String( Lastname.value);
    strLastname= strLastname.charAt(0).toUpperCase() + strLastname.slice(1);
    Lastname.value=strLastname;
        if(strLastname.length>15){
            Lastname.classList.add('is-invalid');
            Lastname.classList.remove('is-valid');
            return false;
        }
        else{
            Lastname.classList.remove('is-invalid');
            Lastname.classList.add('is-valid');
            return true;
        }
        if(strLastname.length==0){
            Lastname.classList.remove('is-invalid');
            Lastname.classList.remove('is-valid');
            return true;
        }
}

function ValidateBirthday(Birthday){

    var minDate= new Date(1900,0,1);

    var date;
      
try{
   date=StringToDate(Birthday.value);
  
   if(date<minDate){
    Birthday.classList.add('is-invalid');
    Birthday.classList.remove('is-valid');
}
else{
    Birthday.classList.remove('is-invalid');
    Birthday.classList.add('is-valid');
}
}catch(error){
    Birthday.classList.add('is-invalid');
}
}

function ValidateITN(ITN){

    var strITN= String(ITN.value);

    if(strITN.length!=12){
        ITN.classList.add('is-invalid');
        ITN.classList.remove('is-valid');
    }
    else{
        ITN.classList.remove('is-invalid');
        ITN.classList.add('is-valid');
    }
}

function ValidatePhoneNumber(PhoneNumber){

    var strPhoneNumber= String(PhoneNumber.value);
    var reg = /\+7 \(\d\d\d\) \d\d\d \d\d-\d\d/;

    if(reg.test(strPhoneNumber)){
        PhoneNumber.classList.remove('is-invalid');
        PhoneNumber.classList.add('is-valid');
    }
    else{
        PhoneNumber.classList.add('is-invalid');
        PhoneNumber.classList.remove('is-valid');
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

function SearchObject(strSerch){

    var surname = strSerch.split(',')[0];
    var name = strSerch.split(',')[1];

    if(typeof(surname)===undefined)
        surname="";
        
    if(typeof(name)===undefined)
        name="";
    
        return{
            name: this.name,
            surname: this.surname
        }
  

}
function SearchContact(){
    $.ajax({
        type: "POST",
        url: "../ContactService.svc/GetContacts",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(SearchObject($("#IdSearch").val())),
        success:  function(response) {
            $("#ContactsTable").attr('disabled', false);
            $.each(response,function(Id, obj)
            {
                var obj = JSON.parse(obj);

                $('#ContactsTable tbody').remove();
              
                for(let i=0; i<obj.length; i++){
                    
                $('#ContactsTable').append('<tr id= "'+obj[i].Id+'" data-toggle="popover" data-content="удалить контакт">'+
                                                '<th scope="row">'+(i+1)+'</th>'+
                                                '<td>'+obj[i].Name+'</td>'+
                                                '<td>'+obj[i].Surname+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].Lastname)+'</td>'+
                                                '<td>'+CodeToSex(obj[i].Sex)+'</td>'+
                                                '<td>'+DateToString( obj[i].Birthday)+'</td>'+
                                                '<td>'+obj[i].ITN+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].PhoneNumber)+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].Post)+'</td>'+
                                                '<td>'+NullToUndefind(obj[i].Job.Name)+'</td>'+
                                                '<td> <button id= "'+obj[i].Id+'" type="button" class="btn btn-outline-primary" onclick="UpdateContactForm(this)">редактировать контакт</button> </td>'+
                                                '<td> <button onclick="DeleteContactForm(this)" id= "'+obj[i].Id+'" type="button" class="btn btn-outline-danger">удалить контакт</button> </td>'+
                                           '</tr>');
                                        
                                       
                              
                }
                
                //$('#ContactsTable').append('<option value="'+obj.Id+'" >'+obj.Name+'</option>');
            });
        }
      });
}
function AddNewContactForm(){
    $('#ContactForm input').each(function(i, obj){
        obj.value="";
        obj.classList.remove('is-valid');
        obj.classList.remove('is-invalid');
    });

    $('#Name').val("")
    $('#Surname').val("")
    $('#Lastname').val("")
    $('#Sex').val("")
    $('#Birthday').val("")
    $('#ITN').val("")
    $('#SurPhonwNumber').val("")
    $('#Post').val("")
    $('#Job').val("")
    $('#ContactId').val("-1");
    $('#exampleModalCenter').modal();
    $('#ContactFormTitle')[0].innerText="Добавьте новый контакт";
}
function UpdateContactForm(options){
    $('#exampleModalCenter').modal();

    $('#ContactFormTitle')[0].innerText="Введите изменения в контакт";
    $('#Name').val( $('#'+options.id+' td')[0].innerHTML);
    $('#ContactId').val(options.id);
    $('#Surname').val( $('#'+options.id+' td')[1].innerHTML);

    if($('#'+options.id+' td')[2].innerHTML==NullToUndefind(null)){
        $('#Lastname').val("");
    }
    else{
        $('#Lastname').val( $('#'+options.id+' td')[2].innerHTML);
    }

    $('#Sex').val( $('#'+options.id+' td')[3].innerHTML);
    $('#Birthday').val( $('#'+options.id+' td')[4].innerHTML);
    $('#ITN').val( $('#'+options.id+' td')[5].innerHTML);

    if( $('#'+options.id+' td')[6].innerHTML==NullToUndefind(null)){
        $('#SurPhonwNumber').val("");
    }
    else{
        $('#SurPhonwNumber').val( $('#'+options.id+' td')[6].innerHTML);
    }

    if( $('#'+options.id+' td')[7].innerHTML==NullToUndefind(null)){
        $('#Post').val("");
    }
    else{
        $('#Post').val( $('#'+options.id+' td')[7].innerHTML);
    }
    $('#Job').val( $('#'+options.id+' td')[8].innerHTML);
}
function DeleteContactForm(contact){
    $('#ContactId').val(contact.id);
    $('#DeleteСonfirm').modal('show');
}
function DeleteContact(contact){
  
    $.ajax({
        type: "POST",
        url: "../ContactService.svc/DeleteContact",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            Id: contact.value
        }),
        success: function(){
            $('#ContactsTable [id= '+contact.value+']').remove();
            alert("контакт успешно удален");
        }
      });
     
      
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
