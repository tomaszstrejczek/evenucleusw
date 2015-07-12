myApp.Blueprint = DS.Model.extend({
    name: DS.attr("string"),
    group: DS.attr("string")
});

myApp.Blueprint.FIXTURES = [
 {
     id: 1,
     name: "Stiletto",
     group: "Ship"
 },
 {
     id: 2,
     name: "Ares",
     group: "Ship"
 },
 {
     id: 3,
     name: "Covert Ops",
     group: "Equipment"
 }
];