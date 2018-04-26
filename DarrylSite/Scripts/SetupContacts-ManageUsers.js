
$(function () {
    console.log("Calling SetRows!");
    $("body").SetRows({
        tablePrefix: "ManageUsers",
        dataStructName: "users",
        newRow:
    '<div class="ManageUsersTableRowContainer">' +
        '<input name="State" class="ManageUsersState" id="users_1__State" type="hidden" value="Clean" data-val-required="The State field is required." data-val="true">' +
        '<div class="row ManageUsersTableRowData">' +
            '<input name="Id" id="users_1__Id" type="hidden" value="">' +
            '<input name="UserName" class="col-md-2 text-box single-line requiredifnotremoved" id="users_1__UserName" type="text" value="">' +
            '<input name="Password" class="col-md-2 text-box single-line password" id="users_1__Password" type="password" value="" data-val="true" data-val-length-min="6" data-val-length-max="100" data-val-length="The Password must be at least 6 characters long.">' +
            '<input name="ConfirmPassword" class="col-md-2 text-box single-line password" id="users_1__ConfirmPassword" type="password" value="" data-val="true" data-val-equalto-other="*.Password" data-val-equalto="The password and confirmation password do not match.">' +
            '<input name="Email" class="col-md-2 text-box single-line requiredifnotremoved" id="users_1__Email" type="email" value="" data-val-required="The Email field is required." data-val="true" data-val-email="The Email field is not a valid e-mail address.">' +
            '<input name="Phone" class="col-md-2 text-box single-line" id="users_1__Phone" type="tel" value="">' +
            '</div>' +
        '</div>',
        valRow:
        '<div class="row ManageUsersTableRowValidation">' +
            '<span name="UserName" class="field-validation-valid text-danger" data-valmsg-replace="true" data-valmsg-for="users[1].UserName"></span>' +
            '<span name="Password" class="field-validation-valid text-danger" data-valmsg-replace="true" data-valmsg-for="users[1].Password"></span>' +
            '<span name="ConfirmPassword" class="field-validation-valid text-danger" data-valmsg-replace="true" data-valmsg-for="users[1].ConfirmPassword"></span>' +
            '<span name="Email" class="field-validation-valid text-danger" data-valmsg-replace="true" data-valmsg-for="users[1].Email"></span>' +
            '<span name="Phone" class="field-validation-valid text-danger" data-valmsg-replace="true" data-valmsg-for="users[1].Phone"></span>' +
        '</div> ',
        formName: 'ManageUsersForm'
    });
});
