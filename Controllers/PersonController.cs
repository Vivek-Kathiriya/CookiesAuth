using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookiesAuth.Data;
using CookiesAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CookiesAuth.Data.Person;

namespace CookiesAuth.Controllers
{
 
    [Authorize]
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            Person dal = new Person();

            var persons = dal.GetAllPerson();

            List<PersonModel> personList = new List<PersonModel>();

            foreach (var person in persons)
            {
                PersonModel person1 = new PersonModel();

                person1.PersonID = person.PersonID;
                person1.Age = person.Age;
                person1.AddressID = person.AddressID;
                person1.Gender = person.Gender;
                person1.FirstName = person.FirstName;
                person1.LastName = person.LastName;
                person1.EmailID = person.EmailID;

                personList.Add(person1);

            }
            return View(personList);
        }


        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PersonDetails person = new PersonDetails();

                    person.FirstName = collection["FirstName"];
                    person.LastName = collection["LastName"];
                    person.Age = Convert.ToInt32(collection["Age"]);
                    person.Gender = collection["Gender"];
                    person.EmailID = collection["EmailID"];
                    person.AddressID = Convert.ToInt32(collection["AddressID"]);


                    Person dal = new Person();

                    dal.InsertPerson(person);

                    RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            Person dal = new Person();

            var personDetails = dal.GetPerson(id);

            PersonModel person = new PersonModel()
            {
                PersonID = personDetails.PersonID,
                FirstName = personDetails.FirstName,
                LastName = personDetails.LastName,
                Age = Convert.ToInt32(personDetails.Age),
                Gender = personDetails.Gender,
                EmailID = personDetails.EmailID,
                AddressID = personDetails.AddressID
            };

            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                PersonDetails person = new PersonDetails();

                person.PersonID = Convert.ToInt32(id);
                person.FirstName = collection["FirstName"];
                person.LastName = collection["LastName"];
                person.Age = Convert.ToInt32(collection["Age"]);
                person.Gender = collection["Gender"];
                person.EmailID = collection["EmailID"];
                person.AddressID = Convert.ToInt32(collection["AddressID"]);

                Person dal = new Person();

                dal.UpdatePerson(person);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        [HttpDelete]
        public ActionResult DeletePerson(int PersonID)
        {
            Person dal = new Person();
            dal.DeletePerson(PersonID);
            return null;
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}