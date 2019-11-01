function InsertOrUpdateContact() {

    ValidateBirthday(document.getElementById("Birthday"));
    ValidateITN(document.getElementById("ITN"));
    ValidatePhoneNumber(document.getElementById("PhoneNumber"));
    ValidateName(document.getElementById("Name"));
    ValidateSurname(document.getElementById("Surname"));
    ValidateLastname(document.getElementById("Lastname"));
       
    if(true){

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

    if((strName.length>15)||(strName.length===0)){
        Name.classList.add('is-invalid');
        Name.classList.remove('is-valid');
        return false;
    }
    if(0>strName.length<15){
        Name.classList.remove('is-invalid');
        Name.classList.add('is-valid');
        return true;
    }
}
function ValidateSurname(Surname){

    var strSurname= String( Surname.value);
    strSurname= strSurname.charAt(0).toUpperCase() + strSurname.slice(1);
    Surname.value=strSurname;

        if((strSurname.length>15)||(strSurname.length===0)){
            Surname.classList.add('is-invalid');
            Surname.classList.remove('is-valid');
            return false;
        }
        if(0>=strSurname.length<15){
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
        if(strLastname.length==0){
            Lastname.classList.remove('is-invalid');
            Lastname.classList.remove('is-valid');
            return false;
        }
        if(0>strLastname.length<15){
            Lastname.classList.remove('is-invalid');
            Lastname.classList.add('is-valid');
            return true;
        }

}

function ValidateBirthday(Birthday){

   
      
try{
    var minDate= new Date(1900,0,1);

    var date;
   
   date=StringToDate(Birthday.value);

   if((date == "Invalid Date")||(date<minDate)){
    Birthday.classList.add('is-invalid');
    Birthday.classList.remove('is-valid');
    return false;
    }
else{
    Birthday.value=DateToString(date);
    Birthday.classList.remove('is-invalid');
    Birthday.classList.add('is-valid');
    return true;
}
}catch(error){
    Birthday.classList.add('is-invalid');
    return false;
}
}
function ValidateITN(ITN){

    var strITN= String(ITN.value);

    if((strITN.length!=12)||(strITN.length==0)||(isNaN(strITN))){
        ITN.classList.add('is-invalid');
        ITN.classList.remove('is-valid');
        return false;
    }
    else{
        ITN.classList.remove('is-invalid');
        ITN.classList.add('is-valid');
        return true;
    }
}

function ValidatePhoneNumber(PhoneNumber){

    var strPhoneNumber= String(PhoneNumber.value);
    if(strPhoneNumber.length==0){
        PhoneNumber.classList.remove('is-invalid');
        PhoneNumber.classList.remove('is-valid');
        return true;}
    var reg = /\+7 \(\d\d\d\) \d\d\d \d\d-\d\d/;

    if(reg.test(strPhoneNumber)){
        PhoneNumber.classList.remove('is-invalid');
        PhoneNumber.classList.add('is-valid');
        return true;
    }
    else{
        PhoneNumber.classList.add('is-invalid');
        PhoneNumber.classList.remove('is-valid');
        return false;
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

    var dateReg = /(\d{1,2})\.(\d{1,2})\.(\d{4})\.?/
    var dd = dateReg.exec(strDate)[1];
    dd = Number(dd);

    var mm = dateReg.exec(strDate)[2];
    mm = Number(mm)-1;

    var yy = dateReg.exec(strDate)[3];
    yy = Number(yy);
  
    return  new Date(yy,mm,dd);
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

    var surnameS = strSerch.split(',')[0];
    var nameS = strSerch.split(',')[1];

    if(typeof(surnameS)===typeof(undefined))
        surnameS="";
        
    if(typeof(nameS)===typeof(undefined))
        nameS="";
    
        return{
            name: nameS,
            surname: surnameS
        };
  

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
              
                var job = null;
                for(let i=0; i<obj.length; i++){
                    job = null;
                  if(obj[i].Job!=null)
                    job= obj[i].Job.Name;
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
                                                '<td>'+NullToUndefind(job)+'</td>'+
                                                '<td> <button id= "'+obj[i].Id+'" type="button" class="btn btn-outline-primary" onclick="UpdateContactForm(this)">редактировать контакт</button> </td>'+
                                                '<td> <button onclick="DeleteContactForm(this)" id= "'+obj[i].Id+'" type="button" class="btn btn-outline-danger">удалить контакт</button> </td>'+
                                           '</tr>');
                                        
                                       
                              
                }          
                //$('#ContactsTable').append('<option value="'+obj.Id+'" >'+obj.Name+'</option>');
            });
        }
      });
}
var workbook1=null;
function GetContactsFile(){
    $.ajax({
        type: "POST",
        url: "../ContactService.svc/GetContactsFile",
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        data: JSON.stringify(SearchObject($("#IdSearch").val())),
        success:  function(response) {
           var workbook = XLSX.read(response.d._buffer, {type:"array"});
            XLSX.writeFile(workbook, "contacts.xlsx");
            workbook1=workbook;
        },
        error: function() {
            alert('error');
        }
      });
}

function UploadContacts(){

   var file = document.getElementById("myFile").files[0];
   var fr = new FileReader();
   fr.onload = receivedText;

   fr.readAsArrayBuffer(file);
   
function receivedText() {

   var result = fr.result;
   var uint8ArrayContacts = new Uint8Array(result);
   var binaryContacts = [...uint8ArrayContacts];

    $.ajax({
		url         : '../ContactService.svc/UploadContact',
		type        : 'POST', // важно!
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            Contacts: binaryContacts})
    })
  }   
}

function AddNewContactForm(){
    $('#ContactForm input').each(function(i, obj){
        obj.value="";
        obj.classList.remove('is-valid');
        obj.classList.remove('is-invalid');
    });


    $('#ContactId').val("-1");
 
    $('#ContactFormTitle')[0].innerText="Добавьте новый контакт";
    $('#exampleModalCenter').modal();
}
function UpdateContactForm(options){
    $('#exampleModalCenter').modal();

    $('#ContactForm input').each(function(i, obj){
        obj.classList.remove('is-valid');
        obj.classList.remove('is-invalid');
    });

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
