// const getInfo = ({ firstName, lastName, age}) => `${firstName} ${lastName}. Age: ${age}`;
// const person1 = {
//     firstName: 'Son Tung',
//     lastName: 'MTP',
//     age: 25
// };
// console.log(getInfo(person1));
// console.log(getInfo({firstName:'Huan', lastName:'Rose'}));
// const person2 = {
//     firstName: 'Son Tung',
//     lastName: 'MTP',
//     age: 25
// };
// console.log(person1 === person2);
// const setPersonName = (person, name) => {
//     person.name = name;
// };
// setPersonName(person1, 'Tung Nui');
// console.log(getInfo(person1));
// console.log(getInfo({...person2, name: 'Tung Nui'}));

const students = [
    { name: "Alex", grade: 15, point: 15 },
    { name: "Devlin", grade: 15, point: 25 },
    { name: "Eagle", grade: 13, point: 12 },
    { name: "Sam", grade: 14, point: 26 },
   ];

//Sort that array by name, grade (Increase and Decrease) then log to console.
students.sort((a, b) => {
    if (a.name < b.name) return -1;
    if (a.name > b.name) return 1;
    return a.grade - b.grade;
});
console.log(students);

//Log all students whose points are greater than 15
const highPointStudents = students.filter(student => student.point > 15);
console.log(highPointStudents);

//Calculate the total point of all students whose grades are equal 15
const totalPoint = students
    .filter(student => student.grade === 15)
    .reduce((total, student) => total + student.point, 0);
console.log(totalPoint);

//Remove the student’s name called “Sam” from the array then log to console
const students1= students.filter(student => student.name !== "Sam");
console.log(students1);