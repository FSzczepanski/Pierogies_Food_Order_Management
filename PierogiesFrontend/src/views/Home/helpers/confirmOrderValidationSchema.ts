import * as yup from "yup";

export default function () {
  return yup.object({
    id: yup.string(),
    purchaserName: yup
      .string()
      .required()
      .min(2)
      .max(150)
      .label("Imię i nazwisko"),
    email: yup.string().required().email().label("e-mail"),
    phone: yup
      .string()
      .min(4)
      .max(16)
      .matches(/^$|\(?\+?[\d \s ( ) -]+$/, "Podaj prawidłowy numer telefonu")
      .required()
      .label("numer telefonu"),
    date: yup.date().required().label("data zamówienia"),
    deliveryPrice: yup.number(),
    formId: yup.string(),
    description: yup.string().nullable().max(200).label("opis"),
    locationDescription: yup.string().nullable().max(200),
    locationName: yup.string().max(150),
    street: yup.string().required().min(2).max(150).label("ulica i numer"),
    zipCode: yup.string().required().min(2).max(20).label("kod pocztowy"),
    cityName: yup.string().required().min(2).max(50).label("miasto"),
    countryName: yup.string(),
    isDefault: yup.boolean(),
    needInvoice: yup.boolean(),
    paymentMethod: yup
      .number()
      .required()
      .label("metoda płatności")
      .min(1),
  });
}
