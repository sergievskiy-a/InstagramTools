webpackJsonp([0],{

/***/ "./angular-app/app/modules/admin/admin-routing.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("./node_modules/@angular/core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_router__ = __webpack_require__("./node_modules/@angular/router/@angular/router.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__main_page_main_page_component__ = __webpack_require__("./angular-app/app/modules/admin/main-page/main-page.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__login_page_login_page_component__ = __webpack_require__("./angular-app/app/modules/admin/login-page/login-page.component.ts");
/* unused harmony export routes */
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AdminRoutingModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var routes = [
    {
        path: '',
        pathMatch: 'full',
        component: __WEBPACK_IMPORTED_MODULE_2__main_page_main_page_component__["a" /* MainPageComponent */]
    },
    {
        path: 'hello',
        component: __WEBPACK_IMPORTED_MODULE_2__main_page_main_page_component__["a" /* MainPageComponent */]
    },
    {
        path: 'login',
        component: __WEBPACK_IMPORTED_MODULE_3__login_page_login_page_component__["a" /* LoginPageComponent */]
    }
];
var AdminRoutingModule = (function () {
    function AdminRoutingModule() {
    }
    return AdminRoutingModule;
}());
AdminRoutingModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
        imports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */].forChild(routes)],
        exports: [__WEBPACK_IMPORTED_MODULE_1__angular_router__["a" /* RouterModule */]],
        providers: []
    })
], AdminRoutingModule);

//# sourceMappingURL=admin-routing.module.js.map

/***/ }),

/***/ "./angular-app/app/modules/admin/admin.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("./node_modules/@angular/core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("./node_modules/@angular/common/@angular/common.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_material__ = __webpack_require__("./node_modules/@angular/material/@angular/material.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__admin_routing_module__ = __webpack_require__("./angular-app/app/modules/admin/admin-routing.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__main_page_main_page_component__ = __webpack_require__("./angular-app/app/modules/admin/main-page/main-page.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__login_page_login_page_component__ = __webpack_require__("./angular-app/app/modules/admin/login-page/login-page.component.ts");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AdminModule", function() { return AdminModule; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var AdminModule = (function () {
    function AdminModule() {
    }
    return AdminModule;
}());
AdminModule = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["b" /* NgModule */])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["h" /* CommonModule */],
            __WEBPACK_IMPORTED_MODULE_3__admin_routing_module__["a" /* AdminRoutingModule */],
            __WEBPACK_IMPORTED_MODULE_2__angular_material__["a" /* MaterialModule */]
        ],
        declarations: [
            __WEBPACK_IMPORTED_MODULE_4__main_page_main_page_component__["a" /* MainPageComponent */], __WEBPACK_IMPORTED_MODULE_5__login_page_login_page_component__["a" /* LoginPageComponent */], __WEBPACK_IMPORTED_MODULE_5__login_page_login_page_component__["b" /* SettingsDialog */]
        ],
        entryComponents: [
            __WEBPACK_IMPORTED_MODULE_4__main_page_main_page_component__["a" /* MainPageComponent */], __WEBPACK_IMPORTED_MODULE_5__login_page_login_page_component__["a" /* LoginPageComponent */], __WEBPACK_IMPORTED_MODULE_5__login_page_login_page_component__["b" /* SettingsDialog */]
        ]
    })
], AdminModule);

//# sourceMappingURL=admin.module.js.map

/***/ }),

/***/ "./angular-app/app/modules/admin/login-page/login-page.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("./node_modules/css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "./angular-app/app/modules/admin/login-page/login-page.component.html":
/***/ (function(module, exports) {

module.exports = "<div [class.dark-theme]=\"isDarkTheme\">\n  <md-toolbar color=\"primary\">\n    <span>\n      <md-icon class=\"icon-20\">pets</md-icon>\n      LeashedIn\n    </span>\n    <button md-icon-button [md-menu-trigger-for]=\"menu\">\n      <md-icon>more_vert</md-icon>\n    </button>\n  </md-toolbar>\n  <md-menu x-position=\"before\" #menu=\"mdMenu\">\n    <button md-menu-item (click)=\"openDialog()\">Settings</button>\n    <button md-menu-item (click)=\"isDarkTheme=!isDarkTheme\">Toggle theme</button>\n    <button md-menu-item>Help</button>\n  </md-menu>\n  <md-sidenav-container>\n    <md-sidenav align=\"end\" mode=\"side\" #sidenav>\n      <md-tab-group>\n        <md-tab>\n          <ng-template md-tab-label>Details</ng-template>\n          <ng-template md-tab-content>\n            <p>Name: {{currentDog.name}}</p>\n            <p>Human: {{currentDog.human}}</p>\n            <p>Age: {{currentDog.age}}</p>\n            <button md-raised-button (click)=\"sidenav.close()\" color=\"accent\">CLOSE</button>\n          </ng-template>\n        </md-tab>\n        <md-tab>\n          <ng-template md-tab-label>Feed</ng-template>\n          <ng-template md-tab-content></ng-template>\n        </md-tab>\n      </md-tab-group>\n    </md-sidenav>\n    <md-grid-list cols=\"4\" rowHeight=\"200px\">\n      <md-grid-tile *ngFor=\"let dog of dogs\" [rowspan]=\"dog.rows\">\n        <img src=\"assets/images/{{dog.name}}.png\" alt=\"photo of {{dog.name}}\">\n        <md-grid-tile-footer>\n          <h3 md-line>{{dog.name}}</h3>\n          <span md-line>Human: {{dog.human}}</span>\n          <button md-icon-button (click)=\"showDog(dog)\">\n            <md-icon>info</md-icon>\n          </button>\n        </md-grid-tile-footer>\n      </md-grid-tile>\n    </md-grid-list>\n  </md-sidenav-container>\n</div>"

/***/ }),

/***/ "./angular-app/app/modules/admin/login-page/login-page.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("./node_modules/@angular/core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_material__ = __webpack_require__("./node_modules/@angular/material/@angular/material.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return SettingsDialog; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return LoginPageComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var SettingsDialog = (function () {
    function SettingsDialog() {
    }
    return SettingsDialog;
}());
SettingsDialog = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["K" /* Component */])({
        selector: 'settings-dialog',
        template: "\n    <label>Would you like dog pics every min???</label>\n    <md-slide-toggle></md-slide-toggle>\n  "
    })
], SettingsDialog);

var LoginPageComponent = (function () {
    function LoginPageComponent(dialog, vcr) {
        this.dialog = dialog;
        this.vcr = vcr;
        this.dogs = [
            { rows: 2, name: "Mal", human: "Jeremy", age: 5 },
            { rows: 1, name: "Molly", human: "David", age: 5 },
            { rows: 1, name: "Sophie", human: "Alex", age: 8 },
            { rows: 2, name: "Taz", human: "Joey", age: '11 weeks' },
            { rows: 1, name: "Kobe", human: "Igor", age: 5 },
            { rows: 2, name: "Porter", human: "Kara", age: 3 },
            { rows: 1, name: "Stephen", human: "Stephen", age: 8 },
            { rows: 1, name: "Cinny", human: "Jules", age: 3 },
            { rows: 1, name: "Hermes", human: "Kara", age: 3 },
        ];
        this.currentDog = {};
        this.isDarkTheme = false;
    }
    LoginPageComponent.prototype.openDialog = function () {
        var config = new __WEBPACK_IMPORTED_MODULE_1__angular_material__["b" /* MdDialogConfig */]();
        config.viewContainerRef = this.vcr;
        this.dialog.open(SettingsDialog, config);
    };
    LoginPageComponent.prototype.showDog = function (dog) {
        this.currentDog = dog;
        this.sidenav.open();
    };
    return LoginPageComponent;
}());
__decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["L" /* ViewChild */])('sidenav'),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["c" /* MdSidenav */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["c" /* MdSidenav */]) === "function" && _a || Object)
], LoginPageComponent.prototype, "sidenav", void 0);
LoginPageComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["K" /* Component */])({
        selector: 'app-login-page',
        template: __webpack_require__("./angular-app/app/modules/admin/login-page/login-page.component.html"),
        styles: [__webpack_require__("./angular-app/app/modules/admin/login-page/login-page.component.css")]
    }),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_1__angular_material__["d" /* MdDialog */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_material__["d" /* MdDialog */]) === "function" && _b || Object, typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_0__angular_core__["t" /* ViewContainerRef */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_0__angular_core__["t" /* ViewContainerRef */]) === "function" && _c || Object])
], LoginPageComponent);

var _a, _b, _c;
//# sourceMappingURL=login-page.component.js.map

/***/ }),

/***/ "./angular-app/app/modules/admin/main-page/main-page.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("./node_modules/css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "./angular-app/app/modules/admin/main-page/main-page.component.html":
/***/ (function(module, exports) {

module.exports = "<p>\r\n  main-page works!\r\n</p>\r\n"

/***/ }),

/***/ "./angular-app/app/modules/admin/main-page/main-page.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("./node_modules/@angular/core/@angular/core.es5.js");
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return MainPageComponent; });
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var MainPageComponent = (function () {
    function MainPageComponent() {
    }
    MainPageComponent.prototype.ngOnInit = function () {
    };
    return MainPageComponent;
}());
MainPageComponent = __decorate([
    __webpack_require__.i(__WEBPACK_IMPORTED_MODULE_0__angular_core__["K" /* Component */])({
        selector: 'app-main-page',
        template: __webpack_require__("./angular-app/app/modules/admin/main-page/main-page.component.html"),
        styles: [__webpack_require__("./angular-app/app/modules/admin/main-page/main-page.component.css")]
    }),
    __metadata("design:paramtypes", [])
], MainPageComponent);

//# sourceMappingURL=main-page.component.js.map

/***/ })

});
//# sourceMappingURL=0.chunk.js.map