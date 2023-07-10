import { createSelector } from "@ngrx/store";
import { selectElements } from "./elements.feature";

export const selectElementsWithNoMenu = createSelector(
    selectElements,
    (elements) => elements.filter(el => !el.menuId)
);

export const selectMenuElement = createSelector(
    selectElements,
    (elements) => elements.find(el => el.menuId !== null)
)