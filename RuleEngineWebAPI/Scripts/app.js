var ViewModel = function () {
    var self = this;
    self.people = ko.observableArray();
    self.rules = ko.observableArray();
    self.policies = ko.observableArray();

    self.error = ko.observable();

    var peopleUri = '/api/People/';
    var rulesUri = '/api/Rules/';
    var policiesUri = '/api/Policies/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }
    
    function getAll(uri, obj) {
        ajaxHelper(uri, 'GET').done(function (data) {
            obj(data);
        });
    }

    
    // Fetch the initial data.
    getAll(peopleUri, self.people);
    getAll(rulesUri, self.rules);
    getAll(policiesUri, self.policies);

    /*
    self.personDetail = ko.observable();

    self.getPersonDetail = function (item) {
        ajaxHelper(peopleUri + item.Id, 'GET').done(function (data) {
            self.personDetail(data);
        });
    }

    self.deleteUserPosition = function (item) {
         ajaxHelper(userpositionsUri + item.Id, 'DELETE').done(function (item) {
             console.log(item);
         });
    }

    self.newUserPosition = {
        CompassDirection: ko.observable(),
        Latitude: ko.observable(),
        Longitude: ko.observable()
    }

    self.addUserPosition = function (formElement) {
        var userposition = {
            CompassDirection: self.newUserPosition.CompassDirection(),
            Latitude: self.newUserPosition.Latitude(),
            Longitude: self.newUserPosition.Longitude()
        };

        ajaxHelper(userpositionsUri, 'POST', userposition).done(function (item) {
            self.userpositions.push(item);
        });
    }*/
};

ko.applyBindings(new ViewModel());