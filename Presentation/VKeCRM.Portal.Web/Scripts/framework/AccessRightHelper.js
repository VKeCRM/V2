var AccessRight = {
    Actionable: 1,
    Viewable: 2,
    Hidden: 3
};

function GetUserAccessRight(accessRightEntity) {
    return AccessRight.Actionable;
}