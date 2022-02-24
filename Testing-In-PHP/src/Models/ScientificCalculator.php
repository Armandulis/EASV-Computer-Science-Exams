<?php

/**
 * NOTE
 * This class is supposed to be used as a model, to connect to a database and get values.
 * Since implementing this is not the focus of the exam project, this class has just simple return statements
 */
namespace App\Models;

/**
 * Class ScientificCalculator
 * @package App\Models
 */
class ScientificCalculator
{
    public static function getConstants(string $scope): array
    {
        return [ 10, 12, 32 ];
    }

    public function saveSubject(string $subject, float $root) : bool
    {
        return true;
    }

    public function calculateSize(int $inputOne, int $inputTwo) : float
    {
        return 2.1;
    }

    public function saveSubjectJson(string $subject, string $string)
    {
    }
}
