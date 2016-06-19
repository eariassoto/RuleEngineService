var ViewModel = function () {
    var self = this;
    self.people = ko.observableArray();
    self.rules = ko.observableArray();
    self.evaluatedPeople = ko.observableArray();
    self.policies = ko.observableArray();

    self.error = ko.observable();

    var peopleUri = '/api/People/';
    var rulesUri = '/api/Rules/';
    var policiesUri = '/api/Policies/';
    var evaluatorUri = '/api/Evaluator/';

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
    getAll(policiesUri, self.policies);

    self.getRules = function (item) {
        getAll(rulesUri + '?Policy=' +item.ID, self.rules);
    }

    self.evaluate = function (item) {
        getAll(evaluatorUri + item.ID, self.evaluatedPeople);
    }

};

ko.applyBindings(new ViewModel());