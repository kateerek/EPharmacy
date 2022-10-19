using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPharmacy.Data.Constants;
using EPharmacy.Data.Entities.ActiveSubstances;
using EPharmacy.Data.Entities.Discounts;
using EPharmacy.Data.Entities.Products;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Attribute = EPharmacy.Data.Entities.Attributes.Attribute;

namespace EPharmacy.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<EPharmacyContext>();
            context.Database.Migrate();

            AddRoles(context, services.GetRequiredService<RoleManager<ApplicationRole>>());
            AddUsers(context, services.GetRequiredService<UserManager<ApplicationUser>>());
            AddPharmacyLocations(context);
            AddProducers(context);
            AddAtributes(context);
            AddProductType(context);
            AddActiveSubstances(context);
            AddPrescriptionsInformation(context);
            AddProducts(context);
            AddDiscountsCategory(context);
            AddDiscounts(context);
            AddProductDiscounts(context);
            AddAttributeDiscounts(context);
        }

        private static async Task CreateRolesAsync(RoleManager<ApplicationRole> roleManager, string[] roles)
        {
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new ApplicationRole(role));
        }

        private static void AddRoles(EPharmacyContext context, RoleManager<ApplicationRole> roleManager)
        {
            if (context.Roles.Any()) return;
            string[] roles = { DefaultRoles.Admin, DefaultRoles.User, DefaultRoles.Worker };
            CreateRolesAsync(roleManager, roles).Wait();
        }

        private struct UserData
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        };

        private static async Task CreateUsersAsync(UserManager<ApplicationUser> userManager, UserData[] users)
        {
            foreach(var userData in users)
            {
                var user = new ApplicationUser
                {
                    UserName = userData.Email,
                    NormalizedUserName = userData.Email.ToUpper(),
                    Email = userData.Email,
                    NormalizedEmail = userData.Email.ToUpper(),
                    FirstName = "firstName",
                    LastName = "lastName"
                };
                await userManager.CreateAsync(user, userData.Password);
                await userManager.AddToRoleAsync(user, userData.Role);
            }
        }

        private static void AddUsers(EPharmacyContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.Users.Any()) return;
            UserData[] users =
            {
                new UserData
                {
                    Email = "admin@email.com",
                    Password = "adminemaiL1!",
                    Role = DefaultRoles.Admin
                },
                new UserData
                {
                    Email = "worker@email.com",
                    Password = "workeremaiL1!",
                    Role = DefaultRoles.Worker,
                },
                new UserData
                {
                    Email = "user@email.com",
                    Password = "useremaiL1!",
                    Role = DefaultRoles.User
                },
                new UserData
                {
                    Email = "user1@email.com",
                    Password = "useremaiL1!",
                    Role = DefaultRoles.User
                },
                new UserData
                {
                    Email = "user2@email.com",
                    Password = "useremaiL1!",
                    Role = DefaultRoles.User
                }
            };
            CreateUsersAsync(userManager, users).Wait();
        }

        private static void AddPharmacyLocations(EPharmacyContext context)
        {
            if (context.PharmacyLocations.Any()) return;
            PharmacyLocation[] locations =
            {
                new PharmacyLocation
                {
                    Name = "Apteka Katedralna",
                    Address = "ul. Sienkiewicza 54/56, 50-351 Wrocław",
                    Latitude = 51.11684775956671,
                    Longitude = 17.052233377538897
                },
                new PharmacyLocation
                {
                    Name = "Apteka Pod Lwami",
                    Address = "Plac Jana Pawla II 7, 50-079 Wrocław",
                    Latitude = 51.11116996166783,
                    Longitude = 17.023651411448675
                }
            };

            context.PharmacyLocations.AddRange(locations);


            context.SaveChanges();
        }

        private static void AddProducers(EPharmacyContext context)
        {
            if (context.Producers.Any()) return;
            Producer[] producers =
            {
                new Producer
                {
                    // Id = 1
                    Address = "18, E. Ozeskienes St. LT-3000 Kaunas, Litwa",
                    Name = "Polpharma"
                },
                new Producer
                {
                    // Id = 2
                    Address = "ul. Partyzancka 133/151, 95-200 Pabianice, Polska",
                    Name = "Aflofarm"
                },
                new Producer
                {
                    // Id = 3
                    Address = "ul. Gen. W. Andersa 38A, 15-113 Białystok, Polska",
                    Name = "Diagnosis"
                }
            };
            context.Producers.AddRange(producers);

            context.SaveChanges();
        }

        private static void AddProductType(EPharmacyContext context)
        {
            if (context.ProductTypes.Any()) return;
            ProductType[] productTypes =
            {
                new ProductType
                {
                    // Id = 1
                    Name = "Medykament",
                    InternalName = "MEDYKAMENT"
                }
            };

            context.ProductTypes.AddRange(productTypes);
            context.SaveChanges();
        }

        private static void AddAtributes(EPharmacyContext context)
        {
            if (context.Attributes.Any()) return;
            Attribute[] attributes =
            {
                new Attribute
                {
                    // Id = 1
                    Name = "Dla dzieci",
                    InternalName = "DLA_DZIECI",
                    Description = "Produkt przeznaczony dla dzieci"
                },
                new Attribute
                {
                    // Id = 2
                    Name = "Dla kobiet w ciąży",
                    InternalName = "DLA_CIĄŻY",
                    Description = "Produkt przeznaczony dla kobiet w ciąży"
                },
                new Attribute
                {
                    // Id = 3
                    Name = "Bez cukru",
                    InternalName = "BEZ_CUKRU",
                    Description = "Produkt bez dodatku cukru"
                },
                new Attribute
                {
                    // Id = 4
                    Name = "Na przeziębienie",
                    InternalName = "NA_PREZIĘBIENIE",
                    Description = "Produkt przeznaczony dla osób chorych na przeziębienie"
                },
                new Attribute()
                {
                    // Id = 5
                    Name = "Preparaty przeciwbólowe",
                    InternalName = "PRZECIWBÓLOWE",
                    Description = "Produkty uśmierzające ból"
                },
                new Attribute()
                {
                    // Id = 6
                    Name = "Układ pokarmowy",
                    InternalName = "UKŁAD_POKARMOWY",
                    Description = "Produkty przeznaczone na dolegliwości związane z układem pokarmowym"
                },
                new Attribute()
                {
                    // Id = 7
                    Name = "Suplement diety",
                    InternalName = "SUPLEMENT_DIETY",
                    Description = "Suplement diety"
                },
                new Attribute()
                {
                    // Id = 8
                    Name = "Różne",
                    InternalName = "RÓŻNE",
                    Description = "Środki medyczne różnego przeznaczenia"
                },
                new Attribute()
                {
                    // Id = 9
                    Name = "Zgaga",
                    InternalName = "NA_ZGAGĘ",
                    Description = "Produkty na zgagę"
                },
                new Attribute()
                {
                    // Id = 10
                    Name = "Alergia i katar sienny",
                    InternalName = "ALERGIA",
                    Description = "Alergia i katar sienny"
                },
                new Attribute()
                {
                    // Id = 11
                    Name = "Układ krążenia",
                    InternalName = "KRĄŻĘNIE",
                    Description = "Układ krążenia"
                },
                new Attribute()
                {
                    // Id = 12
                    Name = "Tabletki",
                    InternalName = "TABLETKI",
                    Description = "Tabletki"
                },
                new Attribute()
                {
                // Id = 13
                Name = "Gorączka",
                InternalName = "goraczka",
                Description = "Produkty w walce z gorączką"
                },
                new Attribute()
                {
                    // Id = 14
                    Name = "Kaszel",
                    InternalName = "kaszel",
                    Description = "Produkty w walce z kaszlem."
                },
            };
            context.Attributes.AddRange(attributes);
            context.SaveChanges();
        }

        private static void AddActiveSubstances(EPharmacyContext context)
        {
            if (context.ActiveSubstances.Any()) return;
                ActiveSubstance[] activeSubstances =
                {
                    // Id = 1
                    new ActiveSubstance
                    {
                        Name =
                            @"5 % TRANS-RETINOL",
                        InternalName =
                            @"5%_TRANS-RETINOL",
                        Description =
                            @"Stymuluje produkcję kolagenu i elastyny, pobudza produkcję kwasu hialuronowego. Reguluje procesy złuszczania i  odnowy naskórka, działa przeciwzapalnie, przyspiesza gojenie i normalizuje wydzielanie sebum",
                    },

                    // Id = 2
                    new ActiveSubstance
                    {
                        Name =
                            @"AKTYWNY WEGIEL",
                        InternalName =
                            @"AKTYWNY_WEGIEL",
                        Description =
                            @"Charakteryzuje się działaniem głęboko oczyszczającym, detoksykującym, antybakteryjnym oraz absorbującym nadmiar sebum.",
                    },

                    // Id = 3
                    new ActiveSubstance
                    {
                        Name =
                            @"ALANTOINA",
                        InternalName =
                            @"ALANTOINA",
                        Description =
                            @"nawilża, zmiękcza i wygładza skórę, łagodzi podrażnienia, przyspiesza gojenie uszkodzonej tkanki niwelując pęknięcia naskórka.",
                    },

                    // Id = 4
                    new ActiveSubstance
                    {
                        Name =
                            @"ALGI CHLORELLA VULGARIS",
                        InternalName =
                            @"ALGI_CHLORELLA_VULGARIS",
                        Description =
                            @"pobudzają syntezę kolagenu i elastyny, odnawiają komórki skóry uszkodzone poprzez nadmierną ekspozycję na promienie słoneczne lub ekspresyjną mimikę twarzy. Redukują zaczerwienienia i opuchliznę pod oczami.",
                    },

                    // Id = 5
                    new ActiveSubstance
                    {
                        Name =
                            @"TAURYNA",
                        InternalName =
                            @"TAURYNA",
                        Description =
                            @"wspomaga produkcję kolagenu, przeciwdziała procesom starzenia się, regeneruje naskórek.",
                    },

                    // Id = 5
                    new ActiveSubstance
                    {
                        Name =
                            @"WITAMINA B3",
                        InternalName =
                            @"WITAMINA_B3",
                        Description =
                            @"Niacinamide PC, wzmacnia skórę, podnosi jej odporność na uszkodzenia. Wyrównuje niedoskonałości skóry wywołane np. trądzikiem. Wykazuje właściwości matujące.",
                    },

                    // Id = 6
                    new ActiveSubstance
                    {
                        Name =
                            @"WITAMINA B5",
                        InternalName =
                            @"WITAMINA_B5",
                        Description =
                            @"nawilża i zmiękcza naskórek, pobudza jego regenerację, poprawia kondycję skóry.",
                    },

                    // Id = 7
                    new ActiveSubstance
                    {
                        Name =
                            @"WITAMINA C",
                        InternalName =
                            @"WITAMINA_C",
                        Description =
                            @"doskonały antyutleniacz, chroni skórę przed wolnymi rodnikami, zwiększa mechaniczną odporność ścian naczyń włosowatych, zmniejsza skłonność do przebarwień. Poprawia mikrocyrkulację krwi, stymuluje syntezę kolagenu. Chroni skórę przed infekcjami i przebarwieniami.",
                    },

                    // Id = 8
                    new ActiveSubstance
                    {
                        Name =
                            @"WITAMINA E",
                        InternalName =
                            @"WITAMINA_E",
                        Description =
                            @"zwalcza wolne rodniki, hamuje procesy starzenia skóry, nawilża, wygładza i zmiękcza naskórek, łagodzi ewentualne stany zapalne, zmniejsza szkodliwe działanie promieni słonecznych na komórki skóry, poprawia kondycję i elastyczność skóry, dodaje skórze witalności i energii.",
                    },

                    
                    // Id = 9
                    new ActiveSubstance
                    {
                        Name =
                            @"WITAMINA F",
                        InternalName =
                            @"WITAMINA_F",
                        Description =
                            @"wpływa na prawidłowy stan skóry i paznokci, przyspiesza regenerację skóry. Odbudowuje płaszcz lipidowy naskórka",
                    },

                    // Id = 10
                    new ActiveSubstance
                    {
                        Name =
                            @"WYSOKOCZĄSTECZKOWY KWAS HIALURONOWY (HMW)",
                        InternalName =
                            @"WYSOKOCZASTECZKOWY_KWAS_HIALURONOWY_HMW",
                        Description =
                            @"Na powierzchni skóry, tworzy warstwę okluzyjną tzw. hydrofilm, zapewniając odpowiedni poziom nawilżenia i zabezpieczając skórę przed utratą elastyczności. Zastosowany Wysokocząsteczkowy Kwas hialuronowy ma masę cząsteczkową 1-1,5 mln kDa.",
                    },

                    // Id = 11
                    new ActiveSubstance
                    {
                        Name =
                            @"ŻEŃ-SZEŃ",
                        InternalName =
                            @"ZEN_SZEN",
                        Description =
                            @"ma działanie regenerujące i odmładzające komórki skóry, posiada właściwości antyoksydacyjne, ponadto zawiera mikro i makroelementy, pierwiastki śladowe i witaminy",
                    },

                    // Id = 12
                    new ActiveSubstance
                    {
                        Name =
                            @"MORSZCZYN",
                        InternalName =
                            @"MORSZCZYN",
                        Description =
                            @"zawiera jod, mannitol, fukoksantynę, brom, kwas alginowy, luminarynę, fukoidynę, całą gamę witamin(B1, B2, C, D, E, H, K, kwas foliowy), mikroelementy (żelazo, mangan, kobalt). Poprawia wygląd skóry, wzmacnia jędrność i elastyczność, aktywizuje procesy regeneracji komórek skóry, przywraca jej naturalne pH. Reguluje czynność gruczołów łojowych, oczyszcza skórę z toksyn, poprawia nawilżenie i wygładza powierzchnię naskórka. Przyspiesza spalanie tkanki tłuszczowej.",
                    },

                    // Id = 13
                    new ActiveSubstance
                    {
                        Name =
                            @"KWAS ASKORBINOWY",
                        InternalName =
                            @"KWAS_ASKORBINOWY",
                        Description =
                            @"Jest przeciwutleniaczem. Aktywuje wiele enzymów, ułatwia asymilację żelaza, wpływa na syntezę kortykosteroidów oraz niektórych neuroprzekaźników. Utrzymuje prawidłowy stan tkanki łącznej (jest niezbędny w syntezie kolagenu), wzmacnia dziąsła i zęby, zabija bakterie wywołujące próchnicę zębów. Wzmacnia odporność organizmu na infekcje. Ułatwia gojenie się ran[34]. Stabilizuje psychikę. Bierze udział w przemianach tyrozyny. Ma również wpływ na zachowanie prawidłowego potencjału oksydacyjnego w komórce",
                    },

                    // Id = 14
                    new ActiveSubstance
                    {
                        Name =
                            @"RUTOZYD",
                        InternalName =
                            @"RUTOZYD",
                        Description =
                            @"Zapobiega powstawaniu niektórych wysoce reaktywnych wolnych rodników. Spowalnia utlenianie witaminy C (przedłuża tym samym jej działanie). Zmniejsza cytotoksyczność utlenionego cholesterolu. Wykazuje też działanie przeciwzapalne. Jest czasami błędnie nazywana witaminą P",
                    },
                    // Id = 15
                    new ActiveSubstance
                    {
                        Name =
                            @"ibuprofen",
                        InternalName =
                            @"ibuprofen",
                        Description =
                            @"Ibuprofen jest powszechnie stosowanym środkiem przeciwbólowym, przeciwzapalnym i przeciwgorączkowym. Charakteryzuje się umiarkowaną siłą działania. Podobnie jak w przypadku innych niesteroidowych leków przeciwzapalnych, mechanizm działania ibuprofenu, polega na hamowaniu aktywności cyklooksygenazy. W rezultacie dochodzi do hamowania syntezy prostaglandyn i w mniejszym stopniu – tromboksanu i prostacyklin. Ibuprofen jest stosowany w leczeniu bólu różnego pochodzenia o słabym lub umiarkowanym nasileniu: bóle głowy (w tym także migreny), zębów, ",
                    },
                    // Id = 16
                    new ActiveSubstance
                    {
                        Name =
                            @"Bromoheksyna",
                        InternalName =
                            @"bromoheksyna",
                        Description =
                            @"romoheksyna jest lekiem o działaniu wykrztuśnym. Występuje w preparatach: Flegamina, Flegatussin. Bromoheksyna to syntetyczna pochodna wazycyny o działaniu mukolitycznym. Działanie bromoheksyny polega głównie na oczyszczaniu oskrzeli z gęstej wydzieliny śluzowej. Bromoheksyna zwiększa objętość wydzieliny śluzowej i zmniejsza jej lepkość. Działanie leku opiera się na depolimeryzacji kwaśnych włókien polisacharydowych w śluzie oraz pobudzeniu aktywności komórek gruczołowych do wytwarzania obojętnych polisacharydów, co skutkuje zmniejszeniem lepkości  ",
                    },
                
                // Id = 13
                //new ActiveSubstance
                //{
                //    Name =
                //        @"",
                //    InternalName =
                //        @"",
                //    Description =
                //        @"",
                //},
            };
            context.ActiveSubstances.AddRange(activeSubstances);
            context.SaveChanges();
        }

        private static void AddPrescriptionsInformation(EPharmacyContext context)
        {
            if (context.PrescriptionInformation.Any()) return;
            PrescriptionInformation[] prescriptionsInformation =
            {
                // Id = 1
                new PrescriptionInformation()
                {
                },
            };
            context.PrescriptionInformation.AddRange(prescriptionsInformation);
            context.SaveChanges();
        }

        private static void AddProducts(EPharmacyContext context)
        {
            if (context.Products.Any()) return;
            Product[] products =
            {
                new Product
                {
                    // Id = 1
                    Name = "Ranigast MAX 150mg, 30 tabletek",
                    ProducerId = 1,
                    ProductTypeId = 1,
                    ProductPrice = 13.98,
                    ProductInformation = new ProductInformation
                    {
                        Composition =
                            @"1 tabletka zawiera: ranitydyna 150mg w postaci ranitydyny chlorowodorku oraz substancje pomocnicze: rdzeń tabletki:krospowidon, magnezu stearynian, celuloza mikrokrystaliczna, krzemionka koloidalna bezwodna, otoczka tabletki: hypromeloza (Methocel E15), lak z żółcienią pomarańczową (E110), tytanu dwutlenek, triacetyna, talk.",
                        IndicationForUse = 
                            @"objawowe leczenie dolegliwości żołądkowych nie związanych z chorobą organiczną przewodu pokarmowego: niestrawność (dyspepsja), zgaga, nadkwaśność, ból w nadbrzuszu.",
                        RecommendedIntake =
                            @"dorośli i dzieci w wieku powyżej 16 lat: w przypadku wystąpienia objawów niestrawności zwykle stosuje się 1 tabletkę 150 mg na dobę. W razie nawrotu dolegliwości można zastosować 1 tabletkę 150 mg 2 razy na dobę. Dobowa dawka leku nie powinna być większa niż 300 mg (2 tabletki).",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/f498164e-185d-4d14-9d12-03288a5da6d2ranigast_max.jpg",
                    AttributesValues = new List<ProductAttributeValue>
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 6,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 7,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {

                    // Id = 2
                    Name = "Pyralgina 500mg, 20 tabletek",
                    ProducerId = 1,
                    ProductTypeId = 1,
                    ProductPrice = 12.98,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 tabletka zawiera: metamizolum natricum 500mg oraz substancje pomocnicze: skrobia ziemniaczana, żelatyna, magnezu stearynian.",
                        IndicationForUse = @"bóle różnego pochodzenia o dużym nasileniu oraz gorączki, gdy zastosowanie innych środków jest przeciwwskazane lub nieskuteczne.",
                        RecommendedIntake = @"bóle występujące sporadycznie: 1-2 tabletek na dobę; bóle przewlekłe: 1-2 tabletek maksymalnie 3 razy na dobę."
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/10cfd00e-3311-4c64-a798-5e7324a37372pyralgina.jpg",
                    AttributesValues = new List<ProductAttributeValue>
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 5,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {
                    // Id = 3
                    Name = "Gastrosan Trawienie, 60 kapsułek",
                    ProducerId = 1,
                    ProductTypeId = 1,
                    ProductPrice = 18.49,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"2 kapsułki zawierają: wyciąg z ziela i korzenia mniszka lekarskiego DER 20:1 60mg, wyciąg z liści karczocha DER 20:1 52,5mg, olejek koprowy 34,12mg, olejek miętowy 3,66mg.",
                        IndicationForUse = "uzupełnienie diety w składniki aktywne preparatu.",
                        RecommendedIntake = "1 kapsułka 2 razy dziennie." 
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/b9250722-92a2-43f5-bbe6-d4e2595ad94cgastrosan_trawienie.jpg",
                    AttributesValues = new List<ProductAttributeValue>
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {
                    // Id = 4
                    Name = "Allertec WZF 10mg, 7 tabletek",
                    ProducerId = 1,
                    ProductTypeId = 1,
                    ProductPrice = 3.95,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 tabletka powlekana zawiera: substancję czynną: cetyryzyny dichlorowodorek 10mg oraz substancje pomocnicze: laktoza jednowodna, celuloza mikrokrystaliczna, skrobia kukurydziana, powidon K-25, magnezu stearynian, karboksymetyloskrobia sodowa, krzemionka koloidalna bezwodna, sodu laurylosiarczan, hypromeloza, makrogol 6000.",
                        IndicationForUse = @"łagodzenie objawów dotyczących nosa i oczu, związanych z sezonowym i przewlekłym alergicznym zapaleniem błony śluzowej nosa (takich jak zatkany nos, duża ilość wodnistej wydzieliny z nosa, częste kichanie, zaczerwienienie oczu, łzawienie, swędzenie oczu); łagodzenie objawów przewlekłej idiopatycznej pokrzywki.",
                        RecommendedIntake = @"dzieci w wieku od 6 do 12 lat 1/2 tabletki 2 razy na dobę. Dorośli i młodzież w wieku powyżej 12 lat 1 tabletka dziennie. Lek można przyjmować przed, w trakcie lub po posiłku. Tabletki należy połykać popijając szklanką wody."
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/6c8f8462-516f-4efc-931c-ddbfed182341allertec.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 10,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {
                    // Id = 5
                    Name = "Rutinacea Complete, 120 tabletek",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 4.89,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"kwas L-askorbinowy, sorbitol (substancja wypełniająca), skrobia kukurydziana, rutozyd, mannitol (substancja wypełniająca), bioflawonoidy cytrusowe, glukonian cynku, aromat, stearynian magnezu (substancja glazurująca), selenian (IV) sodu, poliwinylopirolidon (substancja wiążąca), dwutlenek krzemu (substancja przeciwzbrylająca), aspartam (substancja słodząca), sukraloza (substancja słodząca), acesulfam K (substancja słodząca), sacharyna sodowa (substancja słodząca).",
                        RecommendedIntake = @" 2 tabletka dziennie.",
                        IndicationForUse = @"suplementacja diety w składniki zawarte w preparacie.",
                        Description = @"Rutinacea Complete zawiera rutozyd, witaminę C, cynk i selen oraz bioflawonoidy cytrusowe. Witamina C, selen i cynk pomagają w prawidłowoym funkcjonowaniu układu odpornościowego. Ponadto witamina C przyczynia się do zmniejszenia zmęczenia i znużenia. Selen i cynk pomagają w ochronie komórek przed stresem oksydacyjnym, wywołanym przez wolne rodniki."
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/00bfd2c2-b561-4dbd-81f3-462df2a8d079rutinacea_complete.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {
                    // Id = 6
                    Name = "HepaSlimin, 30 tabletek",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 9.49,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"celuloza (substancja wypełniająca), L-asparaginian L-ornityny, winian choliny, fosforan diwapniowy (substancja wypełniająca), wyciąg z liści mate, wyciąg z ziela karczocha, hydroksypropylometyloceluloza (substancja glazurująca), wyciąg z ostryżu długiego, wyciąg z korzenia cykorii, sole magnezowe kwasów tłuszczowych (substancja glazurująca), dwutlenek tytanu (barwnik), hydroksypropyloceluloza (substancja glazurująca), wosk pszczeli i wosk Carnauba (substancje glazurujące).",
                        RecommendedIntake = @"dorośli: 2 tabletki 3 razy dziennie, popijając wodą. Preparat zaleca się spożyć po posiłku.",
                        IndicationForUse = @"suplementacja diety w składniki aktywne preparatu.",
                        Description = @"Cholina jest składnikiem naturalnie występującym w organizmie. Wchodzi w skład fosfolipidów budujących błonę komórkową każdej żywej komórki. Pomaga w prawidłowym funkcjonowaniu wątroby. Wyciąg z ziela karczocha przyczynia się do prawidłowego funkcjonowania przewodu pokarmowego, wspiera detoksykację, stymuluje wydzielanie soków trawiennych oraz pomaga w utrzymaniu zdrowej wątroby. Przyczynia się do komfortu jelitowego. Wyciąg z korzenia cykorii wspomaga trawienie i pomaga w utrzymaniu zdrowej wątroby. Wyciąg z ostryżu długiego zapobiega gromadzeniu się tłuszczu. Mate pochodząca z liści ostrokrzewu paragwajskiego (Ilex paraguariensis) przyczynia się do rozkładu lipidów i pomaga w utrzymaniu prawidłowej masy ciała."
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/0e9dfe7a-57df-403e-9b3d-a038fb426e82hepaslimin.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {
                    // Id = 7
                    Name = "Diohespan max 1000mg, 60 tabletek",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 31.99,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 tabletka zawiera: substancję czynną: 1000 mg diosminy oraz alkohol poliwinylowy, kroskarmelozę sodową, talk, krzemionkę koloidalną bezwodną, stearynian magnezu.",
                        RecommendedIntake = @"1 tabletka na dobę, podczas posiłku. ",
                        IndicationForUse = @"leczenie żylaków kończyn dolnych. Preparat może być także stosowany w leczeniu objawowym dolegliwości związanych z żylakami odbytu.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/eb6d1046-ce98-49f1-bff7-c6deb2ee14d7diohespan_max.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 11,
                            IsActive = true
                        }
                    }
                },
                new Product()
                {
                    // Id = 8
                    Name = "Paski Abra, 50 sztuk",
                    ProducerId = 3,
                    ProductTypeId = 1,
                    ProductPrice = 38.48,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"Paski Abra to testy paskowe do samodzielnego pomiaru stężenia glukozy we krwi. Są one kompatybilne z glukometrem Abra, który ułatwia samokontrolę i monitorowanie glikemii.
                                        W opakowaniu znajduje się 50 sztuk testów paskowych, każdy z nich jest sterylny i oddzielnie zapakowany w folię.",
                        IndicationForUse = @"Samodzielny pomiar stężenia glukozy we krwi",
                        RecommendedIntake = @"Analiza poziomu cukru odbywa się po umieszczeniu paska w glukometrze i naniesieniu na test odpowiedniej ilości krwi kapilarnej. Optymalna objętość próbki osocza wynosi 0,5 mikrolitra."
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/12323585-93d8-4c91-a09f-f8b84d576078317710328_image.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 8,
                            IsActive = true
                        }
                    }
                },

                new Product()
                {
                    // Id = 9
                    Name = "Rutinoscorbin 25mg+100mg, 150 tabletek",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 11.99,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 tabletka zawiera: substancje czynne: 25mg rutozydu, 100mg kwasu askorbowego oraz substancje pomocnicze: laktozę jednowodną, skrobie ziemniaczaną, sacharozę, alkohol poliwinylowy, talk, magnezu stearynian. W skład otoczki tabletki wchodzi: Opadry II 85F32876 żółty (alkohol poliwinylowy, makrogol 4000, tytanu dwutlenek (E171), talk, żółcień chinolinowa E104).",
                        RecommendedIntake = @"profilaktycznie: 1-2 tabletki na dobę. W stanach niedoboru witaminy C: 1-2 tabletki 2-4 razy na dobę.",
                        IndicationForUse = @"stany niedoboru i zwiększonego zapotrzebowania na witaminę C (przeziębienia, zakażenia wirusowe w tym grypa), pomocniczo w nadmiernej przepuszczalności naczyń.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/67e560f0-255b-4891-8458-c4ada088d2abrutinoscorbi_photo.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 7,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 10,
                            IsActive = true
                        }
                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 13,
                            Amount = (decimal) 100.0,
                        },
                        new ProductActiveSubstance()
                        {
                        ActiveSubstanceId = 14,
                        Amount = (decimal) 25.0,
                        }
                    }
                },

                new Product()
                {
                    // Id = 10
                    Name = "Cerutin 100mg+25mg, 125 tabletek powlekanych",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 5.79,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 tabletka zawiera: 100mg kwasu askorbowego i 25mg rutozydu trójwodnego; pozostałe składniki: celuloza mikrokrystaliczna, skrobia kukurydziana, powidon, magnezu stearynian; otoczka: laktoza jednowodna, hypromeloza, tytanu dwutlenek (E171), makrogol 6000, żelaza tlenek żółty (E172).",
                        RecommendedIntake = @"Dorośli: 1-2 tabletki na dobę. W stanach zwiększonego zapotrzebowania 2-4 razy na dobę 1 lub 2 tabletki.",
                        IndicationForUse = @"Cerutin jest polecany w stanach niedoboru witaminy C i rutyny, a także w celu zapobiegania przeziębieniom i łagodzenia objawów grypy.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/cf3e1866-0835-42bc-9ad2-4b7c116fae72cerutin_photo.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 7,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 10,
                            IsActive = true
                        }
                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 13,
                            Amount = (decimal) 100.0,
                        },
                        new ProductActiveSubstance()
                        {
                        ActiveSubstanceId = 14,
                        Amount = (decimal) 25.0,
                        }
                    }
                },

                new Product()
                {
                    // Id = 11
                    Name = "Vitaminum C 1000 Optimusss, bez cukru, smak cytrynowy, 20 tabletek musujących",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 6.98,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"Regulatory kwasowości: kwas cytrynowy, wodorowęglan sodu; witamina C (kwas L- askorbinowy); substancja wypełniająca: sorbitol; sok cytrynowy (10%) odtworzony z suszonego soku cytrynowego, substancje przeciwzbrylające: glikol polietylenowy, poliwinylopirolidon; aromaty; substancje słodzące: cyklaminian sodu, sacharynian sodu; barwnik: beta-karoten.",
                        RecommendedIntake = @"Dorośli: 1-2 tabletki na dobę. W stanach zwiększonego zapotrzebowania 2-4 razy na dobę 1 lub 2 tabletki.",
                        IndicationForUse = @"Suplementacja diety w witaminę C.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/0343193c-e151-41a9-992e-ae0c710b4deevitaminumC_photo.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 3,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 7,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 10,
                            IsActive = true
                        }
                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 13,
                            Amount = (decimal) 1000.0,
                        },
                    }
                },

                new Product()
                {
                    // Id = 12
                    Name = "Witamina C 600mg Activ, 30 tabletek",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 11.99,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"Kwas L-askorbinowy, celuloza mikrokrystaliczna, stearynian magnezu, talk (substancje przeciwzbrylające); fosforan diwapniowy (substancja wypełniająca), metyloceluloza, hydroksypropylometyloceluloza, hydroksypropyloceluloza (stabilizator), owoc dzikiej róży (proszek), krokosz barwierski (koncentrat), gliceryna (substancja utrzymująca wilgoć), dwutlenek tytanu, tlenek żelaza, koszenila (barwnik).",
                        RecommendedIntake = @"1 tabletka dziennie między posiłkami.",
                        IndicationForUse = @"Uzupełnienie diety w składniki zawarte w produkcie.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/9d4a4e6f-9c7e-4291-a00b-1dc3c5d0d27ewitaminaC_photo.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 7,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 9,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 10,
                            IsActive = true
                        }
                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 13,
                            Amount = (decimal) 600.0,
                        },
                    }
                },

                new Product()
                {
                    // Id = 13
                    Name = "NUROFEN JUNIOR Zawiesina doustna smak truskawkowy - 100 ml",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 19.99,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"substancja czynna: ibuprofenKażdy 1 ml zawiesiny  zawiera 40 mg ibuprofenu. Pozostałe składniki: kwas cytrynowy jednowodny, sodu cytrynian, sodu chlorek, sacharyna sodowa, polisorbat 80, bromek domifenu, malitol ciekły (E965), glicerol, guma ksantan, aromat truskawkowy 500244E (zawierający glikol propylenowy, kwas askorbowy E300 i identyczne z naturalnymi substancje aromatyczne), woda oczyszczona",
                        RecommendedIntake = @"Lek przeznaczony do stosowania doustnego. Przed użyciem mocno wstrząśnij butelką. Użyj końca łyżeczki odpowiedniego dla wymaganej dawki. Wylej lek na łyżeczkę. Po użyciu zakręć butelkę. Umyj łyżeczkę w ciepłej wodzie i pozostaw do wyschnięcia. Zazwyczaj stosowana dawka w przypadku bólu lub gorączki, to: - dzieci od 6 do 9 lat (20 do 30 kg) - 5 ml / 3 razy; - dzieci od 9 do 12 lat (30 do 40 kg) - 7,5 ml (dwukrotne użycie łyżeczki: 5 ml + 2,5 ml) / 3 razy Nie należy przekraczać zalecanej dawki.",
                        IndicationForUse = @"Nurofen Junior Zawiesina doustna smak truskawkowy to lek przeciwbólowy i przeciwgorączkowy adresowany do dzieci od 6. do 12. roku życia. Wskazany do stosowania w przypadku gorączki oraz bólu o nasileniu łagodnym do umiarkowanego.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/e30fd552-0544-49a5-93e2-2198115c0b49nurofen.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 3,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 5,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 12,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 14,
                            IsActive = true
                        },

                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 15,
                            Amount = (decimal) 40.0,
                        },
                    }
                },
                new Product()
                {
                    // Id = 14
                    Name = "MIG DLA DZIECI FORTE Zawiesina doustna 40 mg/ml - 100 ml",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 19.99,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 ml zawiera ibuprofen 40 mg oraz substancje pomocnicze.",
                        RecommendedIntake =
                            @"10 ml, 2 razy na dobę",
                        IndicationForUse =
                            @"Krótkotrwałe leczenie objawowe gorączki, bólu o łagodnym i umiarkowanym nasileniu. Gorączka różnego pochodzenia (także w przebiegu zakażeń wirusowych, w przebiegu odczynu poszczepiennego). Bóle różnego pochodzenia o nasileniu słabym do umiarkowanego: bóle głowy, gardła i mięśni np. w przebiegu zakażeń wirusowych, bóle mięśni, stawów i kości, na skutek urazów narządu ruchu (nadwyrężenia, skręcenia), bóle na skutek urazów tkanek miękkich, bóle pooperacyjne, bóle zębów, bóle po ekstrakcji zębów, bóle na skutek ząbkowania, bóle głowy, bóle uszu występujące w stanach zapalnych ucha środkowego.",
                    },
                    ImageUrl =
                        "https://ziwgstorage.blob.core.windows.net/product-images/39f3626e-87d2-4641-aece-f9847582f042mig_syrop.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 5,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 13,
                            IsActive = true
                        },
                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 15,
                            Amount = (decimal) 40.0,
                        },
                    }
                },
                new Product()
                {
                    // Id = 15
                    Name = "NUROFEN FORTE SYROP DLA DZIECI pomarańczowy 40 mg/ml - 150 ml",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 27.99,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"1 ml zawiesiny zawiera 40 mg ibuprofenu.",
                        RecommendedIntake = @"Lek Nurofen dla dzieci forte o smaku pomarańczowym należy zawsze stosować zgodnie z ulotką lub według zaleceń lekarza. W razie wątpliwości należy zwrócić się do lekarza lub farmaceuty. Lek przeznaczony do stosowania doustnego. Przeznaczony do krótkotrwałego stosowania. Po otwarciu butelki lek należy zużyć w ciągu 6 miesięcy.",
                        IndicationForUse = @"Nurofen dla dzieci forte przeznaczony jest do leczenia gorączki różnego pochodzenia oraz  objawowego, krótkotrwałego leczenia bólu o nasileniu małym do umiarkowanego. Przeznaczony dla dzieci od 3. miesiąca do 12 lat.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/1cbb656b-04d2-4235-903a-64166473b323nurofen_syrop.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 5,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 13,
                            IsActive = true
                        },

                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 15,
                            Amount = (decimal) 40.0,
                        },
                    }
                },
                new Product()
                {
                    // Id = 16
                    Name = "FLEGAMINA JUNIOR Syrop o smaku truskawkowym 2 mg/5 ml - 200 ml",
                    ProducerId = 2,
                    ProductTypeId = 1,
                    ProductPrice = 12.19,
                    ProductInformation = new ProductInformation()
                    {
                        Composition = @"Substancją czynną syropu Flegamina Junior o smaku truskawkowym jest bromoheksyny chlorowodorek. Pozostałe składniki to: kwas cytrynowy jednowodny, glukoza, glicerol, metyl parahydroksybenzoesan, aromat truskawkowy, koszenila w postaci 7,5% roztworu  barwnika, woda oczyszczona.",
                        RecommendedIntake = @"Dzieci w wieku od 2 do 6 lat: 10 ml syropu Flegamina Junior o smaku truskawkowym (odpowiednik 2 łyżeczek do herbaty) 2 razy na dobę. Dzieci od 6 do 12 lat: 10 ml 3 razy na dobę. Dorośli i dzieci powyżej 12 lat: 20 ml syropu 3 razy na dobę. Syrop Flegamina Junior o smaku truskawkowym należy zażywać w równych odstępach czasu, po posiłku. Syropu Flegamina Junior o smaku truskawkowym, ze względu na zawartość bromoheksyny, nie należy podawać dzieciom w wieku poniżej 2 lat.",
                        IndicationForUse = @"Syrop o smaku truskawkowym Flegamina Junior przeznaczony jest do stosowania w przypadku ostrych i przewlekłych chorób dróg oddechowych, przebiegających z zaburzeniami odkrztuszania i usuwania śluzu. Do stosowania u dzieci powyżej 2 roku życia.",
                    },
                    ImageUrl = "https://ziwgstorage.blob.core.windows.net/product-images/61d0edb7-508d-4d88-9874-6c39d5e809d5flegamina_junior_syrop.jpg",
                    AttributesValues = new List<ProductAttributeValue>()
                    {
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 1,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 4,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 5,
                            IsActive = true
                        },
                        new ProductAttributeValue()
                        {
                            AttributeValueId = 13,
                            IsActive = true
                        },

                    },
                    ProductActiveSubstances = new List<ProductActiveSubstance>()
                    {
                        new ProductActiveSubstance()
                        {
                            ActiveSubstanceId = 15,
                            Amount = (decimal) 40.0,
                        },
                    }
                },
            };
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void AddDiscountsCategory(EPharmacyContext context)
        {
            if (context.DiscountCategories.Any()) return;
            DiscountCategory[] discountCategories =
            {
                new DiscountCategory
                {
                    // Id = 1
                    Name = @"IB",
                    Description = @"Właściwe dla inwalidów wojennych oraz osób represjonowanych, ich małżonków pozostający na wyłącznym utrzymaniu oraz wdów i wdowców po poległych żołnierzach i zmarłych inwalidach wojennych oraz osobach represjonowanych, uprawnieni do renty rodzinnej, a także cywilne niewidome ofiary działań wojennych przysługuje bezpłatne zaopatrzenie w leki o kategorii dostępności 'Rp' lub 'Rpz' oraz środki spożywcze specjalnego przeznaczenia żywieniowego objęte decyzją o refundacji, dopuszczone do obrotu na terytorium Rzeczypospolitej Polskiej. Leki recepturowe wydawane są za odpłatnością ryczałtową."
                },
                new DiscountCategory
                {
                    // Id = 2
                    Name = @"IW",
                    Description = @"Należne inwalidom wojskowym przysługuje im bezpłatne, do wysokości limitu finansowania, zaopatrzenie w leki objęte wykazem w zakresie kategorii dostępności refundacyjnej: dostępny w aptece na receptę w całym zakresie zarejestrowanych wskazań i przeznaczeń."
                },
                new DiscountCategory
                {
                    // Id = 3
                    Name = @"ZK",
                    Description = @"Zasłużeni Honorowi Dawcy Krwi oraz Zasłużeni Dawcy Przeszczepu otrzymują bezpłatnie, do wysokości limitu finansowania, leki objęte wykazem w zakresie kategorii dostępności refundacyjnej: dostępny w aptece na receptę w całym zakresie zarejestrowanych wskazań i przeznaczeń oraz leki zgodnie z osobnym wykazem Ministra Zdrowia dla tych pacjentów."
                },
                new DiscountCategory
                {
                    // Id = 4
                    Name = @"PO",
                    Description = @"Osoby wykonujące powszechny obowiązek obrony otrzymują bezpłatnie, do wysokości limitu zaopatrzenie w leki objęte wykazem w zakresie kategorii dostępności refundacyjnej: dostępny w aptece na receptę w całym zakresie zarejestrowanych wskazań i przeznaczeń."
                },
                new DiscountCategory
                {
                    // Id = 5
                    Name = @"AZ",
                    Description = @"Uprawnieni pracownicy i byli pracownicy zakładów produkujących wyroby zawierające azbest otrzymują bezpłatnie leki związane z chorobami wywołanymi pracą przy azbeście wyszczególnione w osobnym wykazie."
                },
                new DiscountCategory
                {
                    // Id = 6
                    Name = @"CN",
                    Description = @"Nieubezpieczone kobiety w okresie ciąży, porodu lub połogu otrzymują leki i wyroby medyczne związane z ciążą, porodem i połogiem na takich samych zasadach jak inni ubezpieczeni."
                },
                new DiscountCategory
                {
                    // Id = 7
                    Name = @"DN",
                    Description = @"Osoby nieubezpieczone, posiadające obywatelstwo polskie i miejsce zamieszkania na terytorium Rzeczypospolitej Polskiej, które nie ukończyły 18 roku życia. Mają one prawo do korzystania ze świadczeń zdrowotnych na takich samych zasadach jak inni ubezpieczeni."
                },
                new DiscountCategory
                {
                    // Id = 8
                    Name = @"IN",
                    Description = @"Osoby nieubezpieczone, posiadające prawo do korzystania ze świadczeń zdrowotnych na podstawie odrębnych przepisów."
                },
                new DiscountCategory
                {
                    // Id = 9
                    Name = @"BW",
                    Description = @"Osoby posiadające prawo do korzystania ze świadczeń opieki zdrowotnej na podstawie decyzji wójta (burmistrza, prezydenta) gminy właściwej ze względu na miejsce zamieszkania."
                },
                new DiscountCategory
                {
                    // Id = 10
                    Name = @"WP",
                    Description = @"Żołnierze pełniący czynną służbę wojskową w razie ogłoszenia mobilizacji i w czasie wojny oraz żołnierze zawodowi, na podstawie odrębnych przepisów."
                },
                new DiscountCategory
                {
                    // Id = 11
                    Name = @"S",
                    Description = @"Grupa leków z przeznaczeniem dla osób w wieku 75 lat i więcej. Są to leki dostępne bezpłatnie, stosowane w leczeniu chorób wieku podeszłego, w tym: chorób serca i układu krążenia, choroby Parkinsona, ostereoporozy czy jaskry."
                },
                new DiscountCategory
                {
                    // Id = 12
                    Name = @"Bezpłatny",
                    Description = @"Ta grupa obejmuje leki i wyroby medyczne, których skuteczność jest udowodniona w leczeniu nowotworu złośliwego, zaburzenia psychotycznego, upośledzenia umysłowego, zaburzenia rozwojowego lub choroby zakaźnej o szczególnym zagrożeniu epidemicznym dla populacji."
                },
                new DiscountCategory
                {
                    // Id = 13
                    Name = @"Ryczałt",
                    Description = @"Ta grupa obejmuje leki, środki spożywcze specjalnego przeznaczenia żywieniowego oraz wyroby medyczne wymagające – zgodnie z aktualną wiedzą medyczną – stosowania dłużej niż 30 dni, których koszt stosowania dla pacjenta przy odpłatności 30% limitu finansowania przekraczałby 5% minimalnego wynagrodzenia za pracę"
                },
                new DiscountCategory
                {
                    // Id = 14
                    Name = @"Odpłatność 30%",
                    Description = @"Ta grupa obejmuje leki, środki spożywcze specjalnego przeznaczenia żywieniowego i wyroby medyczne, które nie zostały zakwalifikowane do innych grup odpłatności."
                },
                new DiscountCategory
                {
                    // Id = 15,
                    Name = @"Odpłatność 50%",
                    Description = @"Ta grupa obejmuje leki, środki spożywcze specjalnego przeznaczenia żywieniowego i wyroby medyczne, które – zgodnie z aktualną wiedzą medyczną – wymagają stosowania nie dłużej niż 30 dni."
                }
            };
            context.DiscountCategories.AddRange(discountCategories);
            context.SaveChanges();
        }

        private static void AddDiscounts(EPharmacyContext context)
        {
            if (context.Discounts.Any()) return;
            Discount[] discounts =
            {
                new Discount
                {
                    // Id = 1
                    Percent = 0.1M,
                    Name = @"10% taniej",
                    Description = @"Promocja Wiosenna 2019 zniżka 10% na produkty przeciwbólowe",
                    ValidTo = new DateTime(2019, 06, 21)
                },
                new Discount
                {
                    // Id = 2
                    Value = 3.22M,
                    Name = "Taniej o 3.22 zł",
                    ValidTo = new DateTime(2019, 06, 27)
                },
                new Discount
                {
                    // Id = 3
                    DiscountCategoryId = 14,
                    Value = 26.86M
                },
                new Discount
                {
                    // Id = 4
                    DiscountCategoryId = 13,
                    Value = 35.17M
                },
                new Discount
                {
                    // Id = 5
                    DiscountCategoryId = 4,
                    Percent = 1M
                }
            };
            context.Discounts.AddRange(discounts);
            context.SaveChanges();
        }

        private static void AddProductDiscounts(EPharmacyContext context)
        {
            if (context.ProductDiscounts.Any()) return;
            ProductDiscount[] discounts =
            {
                new ProductDiscount
                {
                    // Id = 1
                    DiscountId = 3,
                    ProductId = 8
                },
                new ProductDiscount
                {
                    // Id = 2
                    DiscountId = 4,
                    ProductId = 8
                },
                new ProductDiscount
                {
                    // Id = 3
                    DiscountId = 5,
                    ProductId = 8
                },
                new ProductDiscount
                {
                    // Id = 3
                    DiscountId = 2,
                    ProductId = 1
                }
            };
            context.ProductDiscounts.AddRange(discounts);
            context.SaveChanges();
        }

        private static void AddAttributeDiscounts(EPharmacyContext context)
        {
            if (context.AttributeDiscounts.Any()) return;
            AttributeDiscount[] discounts =
            {
                new AttributeDiscount
                {
                    // Id = 1
                    AttributeId = 5,
                    DiscountId = 1
                }
            };
            context.AttributeDiscounts.AddRange(discounts);
            context.SaveChanges();
        }

    }
}