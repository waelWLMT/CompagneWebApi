using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Dtos;

namespace CampaignPubTest.MockData.WebApiMock
{
    public static class AuthenticationMockData
    {
        public static AuthenticateResponse GetAdminIdentity(string userName, string password)
        {

            var menuesList = new List<MenuDto>()
            {
                new MenuDto() {
                    MenuCode= "Acceuil",
                    Link= "tableauDeBord",
                    IconLibelle = "fas fa-home",
                    ParentMenuId= null,
                    IsParent= true,
                    MenuAriaLibelle = "Acceuil",
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls =null,
                    Id= 1,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Gestion Compagne",
                    Link= null,
                    IconLibelle = "fas fa-bullhorn",
                    ParentMenuId= null,
                    IsParent= true,
                    MenuAriaLibelle = "GestionCompagne",
                    DataTarget= "#collapseCompagne",
                    AriaControls = "collapseCompagne",
                    IdAriaControls =null,
                    Id= 2,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Gestion Produits",
                    Link= null,
                    IconLibelle = "fas fa-bullseye",
                    ParentMenuId= null,
                    IsParent= true,
                    MenuAriaLibelle = "GestionProduit",
                    DataTarget= "#collapseProduits",
                    AriaControls = "collapseProduits",
                    IdAriaControls =null,
                    Id= 3,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                // à verifier apres test unitaire
                new MenuDto() {
                    MenuCode= "Gestion Client",
                    Link= null,
                    IconLibelle = "fas fa-user-alt",
                    ParentMenuId= null,
                    IsParent= true,
                    MenuAriaLibelle = "GestionClient",
                    DataTarget= "#collapseClients",
                    AriaControls = "collapseClients",
                    IdAriaControls =null,
                    Id= 4,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Gestion Utilisateur",
                    Link= null,
                    IconLibelle = "fas fa-users",
                    ParentMenuId= null,
                    IsParent= true,
                    MenuAriaLibelle = "GestionUser",
                    DataTarget= "#collapseUsers",
                    AriaControls = "collapseUsers",
                    IdAriaControls =null,
                    Id= 5,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Nouveau utilisateur",
                    Link= "Nve_Utilisateur",
                    IconLibelle = null,
                    ParentMenuId= 5,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = "collapseUsers",
                    IdAriaControls =null,
                    Id= 6,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Utilisateurs",
                    Link= "Lst_Utilisateur",
                    IconLibelle = null,
                    ParentMenuId= 5,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = "collapseUsers",
                    IdAriaControls =null,
                    Id= 7,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Nouvelle compagne",
                    Link= "Nve_Compagne",
                    IconLibelle = null,
                    ParentMenuId= 2,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = "collapseCompagne",
                    IdAriaControls =null,
                    Id= 8,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Compagnes",
                    Link= "Lst_Compagne",
                    IconLibelle = null,
                    ParentMenuId= 2,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = "collapseCompagne",
                    IdAriaControls =null,
                    Id= 9,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Nouveau produit",
                    Link= "Nve_Type_Produit",
                    IconLibelle = null,
                    ParentMenuId= 3,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls ="collapseProduits",
                    Id= 10,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Produits",
                    Link= "Lst_Types_Produits",
                    IconLibelle = null,
                    ParentMenuId= 3,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls ="collapseProduits",
                    Id= 11,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Nouveau client",
                    Link= "Nve_Client",
                    IconLibelle = null,
                    ParentMenuId= 4,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls ="collapseClients",
                    Id= 12,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Clients",
                    Link= "Lst_Client",
                    IconLibelle = null,
                    ParentMenuId= 4,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls ="collapseClients",
                    Id= 13,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Devis",
                    Link= "Lst_Devis_Compagnes",
                    IconLibelle = "fal fa-file-invoice",
                    ParentMenuId= 2,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls ="collapseCompagne",
                    Id= 29,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                },
                new MenuDto() {
                    MenuCode= "Factures",
                    Link= "Lst_Factures_Compagnes",
                    IconLibelle = "fal fa-file-invoice",
                    ParentMenuId= 2,
                    IsParent= false,
                    MenuAriaLibelle = null,
                    DataTarget= null,
                    AriaControls = null,
                    IdAriaControls ="collapseCompagne",
                    Id= 37,
                    CreatedAt= Convert.ToDateTime("2021-04-14T08:41:51.83")
                }
             };
            var user = new AuthenticatedUserDto()
            {
                Id = 1,
                LastName = "Mrabet",
                FirstName = "Wael",
                Matricule = "1234_Mat",
                Email = "wael.mrabet@esprit.tn",
                TelNumber = "56060495",
                ClientId = -1,
                RoleId = 1,
                Menus = menuesList
            };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2NTQ2Nzk0NTgsImV4cCI6MTY1NDc2NTg1OCwiaWF0IjoxNjU0Njc5NDU4fQ.rEJjf_RKSvG6IV64hhjAlCA7CLjrt_wchaxF_8TRDSM";

            return new AuthenticateResponse(user, token);

        }





    }
}
