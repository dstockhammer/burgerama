/// <reference path="angular-ui-bootstrap.d.ts" />
var testApp = angular.module('testApp');

testApp.config(function ($accordionConfig, $buttonConfig, $datepickerConfig, $datepickerPopupConfig, $paginationConfig, $pagerConfig, $progressConfig, $ratingConfig, $timepickerConfig, $tooltipProvider) {
    /**
    * $accordionConfig tests
    */
    $accordionConfig.closeOthers = false;

    /**
    * $buttonConfig tests
    */
    $buttonConfig.activeClass = 'active-state';
    $buttonConfig.toggleEvent = 'dblclick';

    /**
    * $datepickerConfig tests
    */
    $datepickerConfig.dayFormat = 'd';
    $datepickerConfig.dayHeaderFormat = 'E';
    $datepickerConfig.dayTitleFormat = 'dd-MM-yyyy';
    $datepickerConfig.maxDate = '1389586124979';
    $datepickerConfig.minDate = '1389586124979';
    $datepickerConfig.monthFormat = 'M';
    $datepickerConfig.monthTitleFormat = 'yy';
    $datepickerConfig.showWeeks = false;
    $datepickerConfig.startingDay = 1;
    $datepickerConfig.yearFormat = 'y';
    $datepickerConfig.yearRange = 10;

    /**
    * $datepickerPopupConfig tests
    */
    $datepickerPopupConfig.appendToBody = true;
    $datepickerPopupConfig.currentText = 'Select Today';
    $datepickerPopupConfig.clearText = 'Reset Selection';
    $datepickerPopupConfig.closeOnDateSelection = false;
    $datepickerPopupConfig.closeText = 'Finished';
    $datepickerPopupConfig.dateFormat = 'dd-MM-yyyy';
    $datepickerPopupConfig.showButtonBar = false;
    $datepickerPopupConfig.toggleWeeksText = 'Show Weeks';

    /**
    * $paginationConfig tests
    */
    $paginationConfig.boundaryLinks = true;
    $paginationConfig.directionLinks = false;
    $paginationConfig.firstText = 'First Page';
    $paginationConfig.itemsPerPage = 25;
    $paginationConfig.lastText = 'Last Page';
    $paginationConfig.nextText = 'Next Page';
    $paginationConfig.previousText = 'Previous Page';
    $paginationConfig.rotate = false;

    /**
    * $pagerConfig tests
    */
    $pagerConfig.align = false;
    $pagerConfig.itemsPerPage = 25;
    $pagerConfig.nextText = 'Next Page';
    $pagerConfig.previousText = 'Previous Page';

    /**
    * $progressConfig tests
    */
    $progressConfig.animate = false;
    $progressConfig.max = 200;

    /**
    * $ratingConfig tests
    */
    $ratingConfig.max = 10;
    $ratingConfig.stateOff = 'rating-state-off';
    $ratingConfig.stateOn = 'rating-state-on';

    /**
    * $timepickerConfig tests
    */
    $timepickerConfig.hourStep = 2;
    $timepickerConfig.meridians = ['-AM-', '-PM-'];
    $timepickerConfig.minuteStep = 5;
    $timepickerConfig.mousewheel = false;
    $timepickerConfig.readonlyInput = true;
    $timepickerConfig.showMeridian = false;

    /**
    * $tooltipProvider tests
    */
    $tooltipProvider.options({
        placement: 'bottom',
        animation: false,
        popupDelay: 1000,
        appendtoBody: true
    });
    $tooltipProvider.setTriggers({
        'customOpenTrigger': 'customCloseTrigger'
    });
});

testApp.controller('TestCtrl', function ($scope, $log, $modal, $modalStack, $position, $transition) {
    /**
    * test the $modal service
    */
    var modalInstance = $modal.open({
        backdrop: 'static',
        controller: 'ModalTestCtrl',
        keyboard: true,
        resolve: {
            items: function () {
                return [1, 2, 3, 4, 5];
            }
        },
        scope: $scope,
        template: "<div>i'm a template!</div>",
        templateUrl: '/templates/modal.html',
        windowClass: 'modal-test'
    });

    modalInstance.opened.then(function () {
        $log.log('modal opened');
    });

    modalInstance.result.then(function (closeResult) {
        $log.log('modal closed', closeResult);
    }, function (dismissResult) {
        $log.log('modal dismissed', dismissResult);
    });

    /**
    * test the $modalStack service
    */
    $modalStack.open(modalInstance, { scope: $scope });
    $modalStack.close(modalInstance);
    $modalStack.close(modalInstance, 'with reason');
    $modalStack.dismiss(modalInstance);
    $modalStack.dismiss(modalInstance, 'with reason');
    $modalStack.dismissAll();
    $modalStack.dismissAll('with reason');
    $modalStack.getTop().key.close();

    /**
    * test the $position service
    */
    var elementLogger = function (coordinates) {
        $log.log('height', coordinates.height);
        $log.log('left', coordinates.left);
        $log.log('top', coordinates.top);
        $log.log('width', coordinates.width);
    };
    var element = angular.element('<div/>');
    elementLogger($position.position(element));
    elementLogger($position.offset(element));

    /**
    * test the $transition service
    */
    $log.log('animationEndEventName', $transition.animationEndEventName);
    $log.log('transitionEndEventName', $transition.transitionEndEventName);

    var transitionElement = angular.element('<div/>');
    $transition(transitionElement, 'transition-class', { animation: true });
    $transition(transitionElement, { height: '100px', width: '50px' }, { animation: true });
    $transition(transitionElement, function () {
    }, { animation: true });
});

testApp.controller('ModalTestCtrl', function ($scope, $log, $modalInstance, items) {
    items.forEach(function (item) {
        $log.log(item);
    });

    $scope.close = function () {
        if ($scope.useReason) {
            $modalInstance.close('with reason');
        } else {
            $modalInstance.close();
        }
    };

    $scope.dismiss = function () {
        if ($scope.useReason) {
            $modalInstance.dismiss('with reason');
        } else {
            $modalInstance.dismiss();
        }
    };
});
//# sourceMappingURL=angular-ui-bootstrap-tests.js.map
